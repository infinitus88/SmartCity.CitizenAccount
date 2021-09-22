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
                <div class="vx-card__title mb-4">
                  <h4 class="mb-4">Request Payment</h4>
                  <p>Fill the below form to make a payment.</p>
                </div>

                <div class="clearfix">
                  <v-select name="citizen" class="w-full" v-model="selectedCitizenId" :options="citizens" label="fullName">
                    <template v-slot:option="citizen">
                      <vs-avatar class="border-2 border-solid border-white" :src="citizen.photoUrl" size="40px"></vs-avatar>
                      {{ citizen.fullName }}
                    </template>
                  </v-select>

                  <vs-input
                    v-validate="'required'"
                    type="number"
                    data-vv-validate-on="blur"
                    label-placeholder="Amount"
                    name="displayName"
                    placeholder="Amount"
                    v-model="amount"
                    class="w-full"
                  />

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
      fullName: '',
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
