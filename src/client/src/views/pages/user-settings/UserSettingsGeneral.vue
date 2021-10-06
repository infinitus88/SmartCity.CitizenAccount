<template>
  <vx-card no-shadow>

    <vs-popup classContent="popup-example" title="Send Verification Request" :active.sync="verifRequestPopupActive">
      <div class="mb-5">
        <label class="vs-input--label">CitizenId</label>
        <v-select name="citizen" v-model="selectedCitizen" :options="citizens" label="fullName" placeholder="Select your citizenId">
          <template v-slot:option="citizen">
            <vs-avatar class="border-2 border-solid border-white" :src="citizen.photoUrl" size="small"></vs-avatar>
            {{ citizen.fullName }}
          </template>
        </v-select>
        <span class="text-danger text-sm">{{ errors.first('citizen') }}</span>
      </div>
      <vs-button vs-button class="mt-4" :disabled="!validateVerificationForm" @click="sendVerificaitonRequest" color="primary" type="filled">Submit</vs-button>
    </vs-popup>

    <!-- Img Row -->
    <div class="flex flex-wrap items-center mb-base">
      <vs-avatar :src="activeUserInfo.photoUrl" size="70px" class="mr-4 mb-4" />
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
        label-placeholder="Name"
        v-model="name"
        class="w-full mt-8"/>
    <span class="text-danger text-sm">{{ errors.first('name') }}</span>

    <vs-alert v-if="verificationStatus === 'not-sent'" icon-pack="feather" icon="icon-info" class="h-full my-4" color="warning">
      <span>Your account is not verified. <a @click="verifRequestPopupActive = true" class="hover:underline">Send Verification request</a></span>
    </vs-alert>

    <vs-alert v-if="verificationStatus === 'pending'" icon-pack="feather" icon="icon-info" class="h-full my-4" color="success">
      <span>Your request has been successfully sent. Wait for a response</span>
    </vs-alert>

    <vs-alert v-else-if="verificationStatus === 'rejected'">
      <span>Your account is not verified. <a @click="verifRequestPopupActive = true" class="hover:underline">Resend Verification request</a></span>
    </vs-alert>

    <!-- Save & Reset Button -->
    <div class="mt-4 flex flex-wrap items-center justify-end">
      <vs-button class="ml-auto mt-2" :disabled="!validateForm" @action="updateUserInfo">Save Changes</vs-button>
      <vs-button class="ml-4 mt-2" type="border" color="warning" @action="resetFields">Reset</vs-button>
    </div>
  </vx-card>
</template>

<script>
import VueElementLoading from 'vue-element-loading'
import vSelect from 'vue-select'

import moduleCitizen from '@/store/citizen/moduleCitizen.js'
import moduleUser from '@/store/user/moduleUser.js'

export default {
  data () {
    return {
      name: this.$store.state.AppActiveUser.displayName,
      verifRequestPopupActive: false,
      selectedCitizen: ''
    }
  },
  components: {
    VueElementLoading,
    vSelect
  },
  methods: {
    sendVerificaitonRequest () {
      const payload = {
        userId: this.$store.state.AppActiveUser.id,
        citizenId: this.selectedCitizen.id
      }

      this.$store.dispatch('user/sendVerificationRequest', payload)
        .then(() => {
          this.selectedCitizen = ''
          this.verifRequestPopupActive = false 
        })
        .catch(err => { console.error(err) })
    },
    resetFields () {
      this.name = this.$store.state.AppActiveUser.displayName
    },
    updateUserInfo () {
      console.log('dafadsf')
      const payload = {
        displayName: this.name,
        photoUrl: this.activeUserInfo.photoUrl
      }
      this.$store.dispatch('user/updateUserInfo', payload)
    }
  },
  computed: {
    validateVerificationForm () {
      return !this.errors.any() && this.selectedCitizenId !== ''
    },
    citizens () {
      return this.$store.getters['citizen/getCitizens']
    },
    verificationStatus () {
      return this.$store.getters['user/getVerificationStatus']
    },
    validateForm () {
      return !this.errors.any() && this.name !== ''
    },
    activeUserInfo () {
      return this.$store.state.AppActiveUser
    },
    isLoading () {
      return this.$store.getters['user/isLoading']
    }
  },
  created () {
    if (!moduleCitizen.isRegistered) {
      this.$store.registerModule('citizen', moduleCitizen)
      moduleCitizen.isRegistered = true
    }
    if (!moduleUser.isRegistered) {
      this.$store.registerModule('user', moduleUser)
      moduleUser.isRegistered = true
    }

    this.$store.dispatch('citizen/fetchCitizens') // Fetch Citizens From API
    this.$store.dispatch('user/fetchVerificationStatus').catch((err) => { console.log(err) })
  },
  beforeDestroy () {
    // this.$store.unregisterModule('citizen')
  }
}
</script>
