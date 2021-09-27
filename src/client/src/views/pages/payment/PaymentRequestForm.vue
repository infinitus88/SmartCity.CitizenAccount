<template>
  <div
    class="
      h-screen
      flex
      w-full
      bg-img
      vx-row
      no-gutter
      items-center
      justify-center
    "
  >
    <div class="vx-col sm:w-1/2 md:w-1/2 lg:w-3/4 xl:w-2/5 sm:m-0 m-4">
      <vx-card>
            <div
              class="
                vx-col
                sm:w-full
                md:w-full
                lg:w-2/2
                mx-auto
                self-center
                d-theme-dark-bg
              "
            >
              <div class="px-8 pt-8 register-tabs-container">
                <div>
                </div>
                <div class="vx-card__title mb-4" align="center">
                  <img :src="require('@/assets/images/logo/logo.png')"/>
                  <h4 class="mb-4">Request Payment</h4>
                  <p>Fill the below form to make a payment.</p>
                </div>

                <div class="clearfix">
                  <div class="mt-4">
                    <label class="vs-input--label">CitizenId</label>
                    <v-select name="citizen" v-model="selectedCitizenId" :options="citizens" label="fullName" placeholder="Select Citizen">
                      <template v-slot:option="citizen">
                        <vs-avatar class="border-2 border-solid border-white" :src="citizen.photoUrl" size="40px"></vs-avatar>
                        {{ citizen.fullName }}
                      </template>
                    </v-select>
                    <!-- <span class="text-danger text-sm"  v-show="errors.has('status')">{{ errors.first('status') }}</span> -->
                  </div>

                  <div class="mt-4">
                    <label class="vs-input--label">Amount</label>
                    <vs-input
                      disabled
                      icon-pack="feather"
                      icon="icon-dollar-sign"
                      v-model="amount"
                      class="w-full"
                      v-validate="{ required: true, regex: /\d+(\.\d+)?$/ }"
                      name="amount" 
                    />
                    <span class="text-danger text-sm" v-show="errors.has('amount')">{{ errors.first('amount') }}</span>
                  </div>

                  <vs-button
                    type="border"
                    class="float-left mt-6"
                    @click="proceedPayment()"
                    >Cancel</vs-button
                  >

                  <vs-button
                    class="float-right mt-6"
                    @click="proceedPayment()"
                    >Proceed</vs-button
                  >
                </div>
              </div>
            </div>
      </vx-card>
    </div>
  </div>
</template>

<script>
import vSelect from 'vue-select'
import moduleCitizen from '@/store/citizen/moduleCitizen.js'

export default {


  data () {
    return {
      fullName: 'Enter CitizenId',
      selectedCitizenId: '',
      amount: this.$route.query.amount,
      orderId: this.$route.query.orderId,
      serviceId: this.$route.query.serviceId
    }
  },
  methods: {
    clearFields () {
      this.$nextTick(() => {
        this.fullName = ''
        this.amount = ''
      })
    },
    proceedPayment () {
      const payload = {
        citizenId: this.selectedCitizenId,
        amount: this.amount
      }
      this.$store.dispatch('citizen/proceedPayment', payload)

      this.clearFields()
    }
  },
  computed: {
    citizens () {
      return this.$store.getters['citizen/getCitizens']
    }
  },
  components: {
    vSelect
  },
  created () {
    this.$store.registerModule('citizen', moduleCitizen)

    this.$store.dispatch('citizen/fetchCitizens') // Fetch Citizens From API
  },
  beforeDestroy () {
    this.$store.unregisterModule('citizen')
  }
}
</script>

<style lang="scss">
.register-tabs-container {
  min-height: 517px;

  .con-tab {
    padding-bottom: 23px;
  }
}
</style>
