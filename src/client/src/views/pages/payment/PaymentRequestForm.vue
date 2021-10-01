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
    <div class="vx-col sm:w-1/2 md:w-1/2 lg:w-3/4 xl:w-1/4 sm:m-0 m-4">
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
                        <vs-avatar class="" :src="citizen.photoUrl" size="small"></vs-avatar>
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
                    @click="cancelPayment()"
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
import modulePayment from '@/store/payment/modulePayment.js'

export default {


  data () {
    return {
      fullName: 'Enter CitizenId',
      selectedCitizenId: '',
      amount: this.$route.query.amount,
      orderId: this.$route.query.orderId,
      serviceId: this.$route.query.serviceId,

      serviceInfo: {
        3: { 
          'successUrl': '', 
          'cancelUrl': '',
          'mark-paid': ''
        }
      }
    }
  },
  methods: {
    clearFields () {
      this.$nextTick(() => {
        this.fullName = ''
        this.amount = ''
      })
    },
    redirectToSuccessPage () {
      window.location = 'http://catering-frontend.azurewebsites.net/checkout/result/true'
    },
    cancelPayment () {
      this.$vs.loading()
      window.location = 'https://catering-frontend.azurewebsites.net/checkout/result/false'
    },
    proceedPayment () {
      this.$vs.loading()

      const payload = {
        citizenId: this.selectedCitizenId,
        amount: parseFloat(this.amount),
        serviceId: parseInt(this.serviceId)
      }
      // this.$store.dispatch('citizen/proceedPayment', payload)
      //   .then(() => { 
      //     this.$vs.loading.close() 
      //     this.$vs.notify({
      //       title: 'Success',
      //       text: 'Payment was successful',
      //       iconPack: 'feather',
      //       icon: 'icon-alert-circle',
      //       color: 'success'
      //     })
          
      //   })
      //   .catch(error => {
      //     this.$vs.loading.close()
      //     this.$vs.notify({
      //       title: 'Error',
      //       text: error.message,
      //       iconPack: 'feather',
      //       icon: 'icon-alert-circle',
      //       color: 'danger'
      //     })
      //   })
      this.redirectToSuccessPage()
      // this.clearFields()
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
    this.$vs.loading()
    if (!moduleCitizen.isRegistered) {
      this.$store.registerModule('citizen', moduleCitizen)
      moduleCitizen.isRegistered = true
    }

    if (!modulePayment.isRegistered) {
      this.$store.registerModule('payment', modulePayment)
      modulePayment.isRegistered = true
    }

    this.$store.dispatch('citizen/fetchCitizens')
      .then(() => { this.$vs.loading.close() })
      .catch(err => { console.error(err) })
  },
  beforeDestroy () {
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
