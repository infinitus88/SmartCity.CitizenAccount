<template>
  <vx-card no-shadow>

    <!-- Img Row -->
    <div class="flex flex-wrap items-center mb-base">
      <vs-avatar :src="activeUserInfo.photoURL" size="70px" class="mr-4 mb-4" />
      <div>
        <vs-button class="mr-4 sm:mb-0 mb-2">Upload photo</vs-button>
        <vs-button type="border" color="danger">Remove</vs-button>
        <p class="text-sm mt-2">Allowed JPG, GIF or PNG. Max size of 800kB</p>
      </div>
    </div>

    <!-- Info -->
    <vs-input
        v-validate="'required|min:3'"
        data-vv-validate-on="blur"
        name="name"
        label-placeholder="Email"
        v-model="name"
        class="w-full mt-8"/>
    <span class="text-danger text-sm">{{ errors.first('name') }}</span>

    <!-- Save & Reset Button -->
    <div class="flex flex-wrap items-center justify-end">
      <vs-button class="ml-auto mt-2" :disabled="!validateForm" @action="updateUserInfo">Save Changes</vs-button>
      <vs-button class="ml-4 mt-2" type="border" color="warning" @action="resetFields">Reset</vs-button>
    </div>
  </vx-card>
</template>

<script>
import VueElementLoading from 'vue-element-loading'

export default {
  data () {
    return {
      name: this.$store.state.AppActiveUser.displayName
    }
  },
  components: {
    VueElementLoading
  },
  methods: {
    resetFields () {
      this.name = this.$store.state.AppActiveUser.displayName
    },
    updateUserInfo () {
      const payload = {
        displayName: this.name,
        photoURL: this.activeUserInfo.photoURL
      }
      this.$store.dispatch('user/updateUserInfo', payload)
    }
  },
  computed: {
    validateForm () {
      return !this.errors.any() && this.name !== ''
    },
    activeUserInfo () {
      return this.$store.state.AppActiveUser
    },
    isLoading () {
      return this.$store.getters['user/isLoading']
    }
  }
}
</script>
