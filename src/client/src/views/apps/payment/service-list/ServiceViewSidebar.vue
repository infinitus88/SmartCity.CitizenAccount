<template>
  <vs-sidebar click-not-close position-right parent="body" default-index="1" color="primary" class="add-new-data-sidebar items-no-padding" spacer v-model="isSidebarActiveLocal">
    <div class="mt-6 flex items-center justify-between px-6">
        <h4>{{ Object.entries(this.data).length === 0 ? "ADD NEW" : "UPDATE" }} SERVICE</h4>
        <feather-icon icon="XIcon" @click.stop="isSidebarActiveLocal = false" class="cursor-pointer"></feather-icon>
    </div>
    <vs-divider class="mb-0"></vs-divider>

    <component :is="scrollbarTag" class="scroll-area--data-list-add-new" :settings="settings" :key="$vs.rtl">

      <div class="p-6">

        <!-- Product Image -->
        <template v-if="imageUrl">

          <!-- Image Container -->
          <div class="img-container w-64 mx-auto flex items-center justify-center">
            <img :src="imageUrl" alt="img" class="responsive">
          </div>

          <!-- Image upload Buttons -->
          <div class="modify-img flex justify-between mt-5">
            <input type="file" class="hidden" ref="updateImgInput" @change="updateCurrImg" accept="image/*">
            <vs-button class="mr-4" type="flat" @click="$refs.updateImgInput.click()">Update Image</vs-button>
            <vs-button type="flat" color="#999" @click="imageUrl = null">Remove Image</vs-button>
          </div>
        </template>

        <!-- NAME -->
        <vs-input label="Name" v-model="name" class="mt-5 w-full" name="item-name" v-validate="'required'" />
        <span class="text-danger text-sm" v-show="errors.has('item-name')">{{ errors.first('item-name') }}</span>

        <!-- EMAIL -->
        <vs-input type="email" label="Email" v-model="email" class="mt-5 w-full" name="item-email" v-validate="'required'" />
        <span class="text-danger text-sm" v-show="errors.has('item-email')">{{ errors.first('item-email') }}</span>

        <!-- MARK PAID REQUEST URL -->
        <vs-input label="MarkPaid Request Url" v-model="markPaidRequestUrl" class="mt-5 w-full" name="item-mark-paid-request-url" v-validate="'required'" />
        <span class="text-danger text-sm" v-show="errors.has('item-mark-paid-request-url')">{{ errors.first('item-mark-paid-request-url') }}</span>

        <!-- SUCCESS REDIRECT URL -->
        <vs-input label="Success Redirect Url" v-model="successRedirectUrl" class="mt-5 w-full" name="item-success-redirect-url" v-validate="'required'" />
        <span class="text-danger text-sm" v-show="errors.has('item-success-redirect-url')">{{ errors.first('item-success-redirect-url') }}</span>

        <!-- SUCCESS REDIRECT URL -->
        <vs-input label="Cancel Redirect Url" v-model="cancelRedirectUrl" class="mt-5 w-full" name="item-cancel-redirect-url" v-validate="'required'" />
        <span class="text-danger text-sm" v-show="errors.has('item-cancel-redirect-url')">{{ errors.first('item-cancel-redirect-url') }}</span>

        <!-- Upload -->
        <!-- <vs-upload text="Upload Image" class="img-upload" ref="fileUpload" /> -->

        <div class="upload-img mt-5" v-if="!imageUrl">
          <input type="file" class="hidden" ref="uploadImgInput" @change="updateCurrImg" accept="image/*">
          <vs-button @click="$refs.uploadImgInput.click()">Upload Image</vs-button>
        </div>
      </div>
    </component>

    <div class="flex flex-wrap items-center p-6" slot="footer">
      <vs-button class="mr-6" @click="submitData" :disabled="!isFormValid">Submit</vs-button>
      <vs-button type="border" color="danger" @click="isSidebarActiveLocal = false">Cancel</vs-button>
    </div>
  </vs-sidebar>
</template>

<script>
import VuePerfectScrollbar from 'vue-perfect-scrollbar'

export default {
  props: {
    isSidebarActive: {
      type: Boolean,
      required: true
    },
    data: {
      type: Object,
      default: () => {}
    }
  },
  components: {
    VuePerfectScrollbar
  },
  data () {
    return {

      id: null,
      name: '',
      email: '',
      imageUrl: null,
      markPaidRequestUrl: '',
      successRedirectUrl: '',
      cancelRedirectUrl: '',

      settings: { // perfectscrollbar settings
        maxScrollbarLength: 60,
        wheelSpeed: .60
      }
    }
  },
  watch: {
    isSidebarActive (val) {
      if (!val) return
      if (Object.entries(this.data).length === 0) {
        this.initValues()
        this.$validator.reset()
      } else {
        const { id, imageUrl, name, email, markPaidRequestUrl, successRedirectUrl, cancelRedirectUrl } = JSON.parse(JSON.stringify(this.data))
        this.id = id
        this.email = email
        this.imageUrl = imageUrl
        this.name = name
        this.markPaidRequestUrl = markPaidRequestUrl
        this.successRedirectUrl = successRedirectUrl
        this.cancelRedirectUrl = cancelRedirectUrl
        this.initValues()
      }
      // Object.entries(this.data).length === 0 ? this.initValues() : { this.dataId, this.dataName, this.dataCategory, this.dataOrder_status, this.dataPrice } = JSON.parse(JSON.stringify(this.data))
    }
  },
  computed: {
    isSidebarActiveLocal: {
      get () {
        return this.isSidebarActive
      },
      set (val) {
        if (!val) {
          this.$emit('closeSidebar')
          // this.$validator.reset()
          // this.initValues()
        }
      }
    },
    isFormValid () {
      return !this.errors.any() && this.name && this.email && this.markPaidRequestUrl && this.successRedirectUrl && this.cancelRedirectUrl
    },
    scrollbarTag () { return this.$store.getters.scrollbarTag }
  },
  methods: {
    initValues () {
      if (this.data.id) return
      this.id = null
      this.name = ''
      this.email = ''
      this.imageUrl = ''
      this.markPaidRequestUrl = ''
      this.successRedirectUrl = ''
      this.cancelRedirectUrl = ''
    },
    submitData () {
      const payload = {
        id: this.id,
        name: this.name,
        email: this.email,
        imageUrl: this.imageUrl,
        markPaidRequestUrl: this.markPaidRequestUrl,
        successRedirectUrl: this.successRedirectUrl,
        cancelRedirectUrl: this.cancelRedirectUrl
      }

      if (this.id !== null && this.id >= 0) {
        this.$store.dispatch('payment/updateService', payload)
          .then(() => { 
            this.$parent.gridApi.redrawRows()
          })
          .catch(err => { console.error(err) })
      } else {
        delete payload.id
        this.$store.dispatch('payment/addService', payload).catch(err => { console.error(err) })
      }

      this.$emit('closeSidebar')
      this.initValues()
    },
    updateCurrImg (input) {
      if (input.target.files && input.target.files[0]) {
        const reader = new FileReader()
        reader.onload = e => {
          this.imageUrl = e.target.result
        }
        reader.readAsDataURL(input.target.files[0])
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.add-new-data-sidebar {
  ::v-deep .vs-sidebar--background {
    z-index: 52010;
  }

  ::v-deep .vs-sidebar {
    z-index: 52010;
    width: 400px;
    max-width: 90vw;

    .img-upload {
      margin-top: 2rem;

      .con-img-upload {
        padding: 0;
      }

      .con-input-upload {
        width: 100%;
        margin: 0;
      }
    }
  }
}

.scroll-area--data-list-add-new {
    // height: calc(var(--vh, 1vh) * 100 - 4.3rem);
    height: calc(var(--vh, 1vh) * 100 - 16px - 45px - 82px);

    &:not(.ps) {
      overflow-y: auto;
    }
}
</style>
