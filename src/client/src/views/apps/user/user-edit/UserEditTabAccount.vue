<template>
  <div id="user-edit-tab-info">
    <vue-element-loading :active="isLoading" size="50" />
    <!-- Avatar Row -->
    <div class="vx-row">
      <div class="vx-col w-full">
        <div class="flex items-start flex-col sm:flex-row">
          <img :src="data.photoUrl" class="mr-8 rounded h-24 w-24" />
          <!-- <vs-avatar :src="data.avatar" size="80px" class="mr-4" /> -->
          <div>
            <p class="text-lg font-medium mb-2 mt-4 sm:mt-0">{{ data.name  }}</p>
            <input type="file" class="hidden" ref="update_avatar_input" @change="update_avatar" accept="image/*">

            <!-- Toggle comment of below buttons as one for actual flow & currently shown is only for demo -->
            <vs-button class="mr-4 mb-4">Change Avatar</vs-button>
            <!-- <vs-button type="border" class="mr-4" @click="$refs.update_avatar_input.click()">Change Avatar</vs-button> -->

            <vs-button type="border" color="danger">Remove Avatar</vs-button>
          </div>
        </div>
      </div>
    </div>

    <!-- Content Row -->
    <div class="vx-row">
      <div class="vx-col md:w-1/2 w-full">
        <vs-input class="w-full mt-4" label="Name" v-model="data_local.displayName" v-validate="'required|alpha_spaces'" name="name" />
        <span class="text-danger text-sm"  v-show="errors.has('name')">{{ errors.first('name') }}</span>

        <div class="mt-4">
          <label class="vs-input--label">CitizenId</label>
          <v-select name="citizen" v-model="data_local.citizenId" :options="citizens" label="fullName">
            <template v-slot:option="citizen">
              <vs-avatar class="border-2 border-solid border-white" :src="citizen.photoUrl" size="40px"></vs-avatar>
              {{ citizen.fullName }}
            </template>
          </v-select>
          <span class="text-danger text-sm"  v-show="errors.has('status')">{{ errors.first('status') }}</span>
        </div>
      </div>

      <div class="vx-col md:w-1/2 w-full">

        <div class="mt-4">
          <label class="vs-input--label">Status</label>
          <v-select v-model="status_local" :clearable="false" :options="statusOptions" v-validate="'required'" name="status" :dir="$vs.rtl ? 'rtl' : 'ltr'" />
          <span class="text-danger text-sm"  v-show="errors.has('status')">{{ errors.first('status') }}</span>
        </div>

        <div class="mt-4">
          <label class="vs-input--label">Role</label>
          <v-select v-model="role_local" :clearable="false" :options="roleOptions" v-validate="'required'" name="role" :dir="$vs.rtl ? 'rtl' : 'ltr'" />
          <span class="text-danger text-sm"  v-show="errors.has('role')">{{ errors.first('role') }}</span>
        </div>

      </div>
    </div>

    <!-- Save & Reset Button -->
    <div class="vx-row">
      <div class="vx-col w-full">
        <div class="mt-8 flex flex-wrap items-center justify-end">
          <vs-button class="ml-auto mt-2" @click="save_changes" :disabled="!validateForm">Save Changes</vs-button>
          <vs-button class="ml-4 mt-2" type="border" color="warning" @click="reset_data">Reset</vs-button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import vSelect from 'vue-select'
import VueElementLoading from 'vue-element-loading'

export default {
  components: {
    vSelect,
    VueElementLoading
  },
  props: {
    data: {
      type: Object,
      required: true
    }
  },
  data () {
    return {

      data_local: JSON.parse(JSON.stringify(this.data)),

      statusOptions: [
        { label: 'Active',  value: 'active' },
        { label: 'Deactivated',  value: 'deactivated' }
      ],
      roleOptions: [
        { label: 'Admin',  value: 'admin' },
        { label: 'User',  value: 'user' }
      ]
    }
  },
  computed: {
    isLoading () {
      return this.$store.getters['userManagement/isLoading']
    },
    status_local: {
      get () {
        return { label: this.capitalize(this.data_local.status),  value: this.data_local.status  }
      },
      set (obj) {
        this.data_local.status = obj.value
      }
    },
    role_local: {
      get () {
        return { label: this.capitalize(this.data_local.role),  value: this.data_local.role  }
      },
      set (obj) {
        this.data_local.role = obj.value
      }
    },
    validateForm () {
      return !this.errors.any()
    },
    citizens () {
      return this.$store.getters['citizen/getCitizens']
    }
  },
  methods: {
    capitalize (str) {
      return str.slice(0, 1).toUpperCase() + str.slice(1, str.length)
    },
    save_changes () {
      const payload = this.data_local

      this.$store.dispatch('userManagement/updateUser', payload)
        .then(res => { 
          this.data_local = res.data
          this.$router.push('/apps/user/user-list').catch({})
        })
    },
    reset_data () {
      this.data_local = JSON.parse(JSON.stringify(this.data))
    },
    update_avatar () {
      // You can update avatar Here
      // For reference you can check dataList example
      // We haven't integrated it here, because data isn't saved in DB
    }
  }
}
</script>
