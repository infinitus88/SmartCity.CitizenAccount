<template>
  <div>
    <vs-alert color="danger" title="Invoice Not Found" :active.sync="invoice_not_found">
      <span>Invoice with id: {{ $route.params.invoiceId }} not found. </span>
      <span>
      <span>Check </span><router-link :to="{name:'invoice-list'}" class="text-inherit underline">All Invoices</router-link>
      </span>
    </vs-alert>
    <div id="invoice-page" v-if="invoice_data">

        <div class="flex flex-wrap items-center justify-between">
          <vx-input-group class="mb-base mr-3">
            <vs-input v-model="mailTo" placeholder="Email" />

            <template slot="append">
              <div class="append-text btn-addon">
                <vs-button type="border" @click="mailTo = ''" class="whitespace-no-wrap">Send Invoice</vs-button>
              </div>
            </template>
          </vx-input-group>
          <div class="flex items-center">
            <vs-button class="mb-base mr-3" type="border" icon-pack="feather" icon="icon icon-download">Download</vs-button>
            <vs-button class="mb-base mr-3" icon-pack="feather" icon="icon icon-file" @click="printInvoice">Print</vs-button>
          </div>
        </div>

        <vx-card id="invoice-container">

            <!-- INVOICE METADATA -->
            <div class="vx-row leading-loose p-base">
                <div class="vx-col w-1/2 mt-base">
                    <img :src="invoice_data.serviceImage" alt="service-logo">
                </div>
                <div class="vx-col w-1/2 text-right">
                    <h1>Invoice</h1>
                    <div class="invoice__invoice-detail mt-6">
                        <h6>INVOICE NO.</h6>
                        <p>{{ invoice_data.id }}</p>

                        <h6 class="mt-4">INVOICE DATE</h6>
                        <p>{{ invoice_data.creationDate | date(true) }}</p>
                    </div>
                </div>
                <template v-if="invoice_data.invoiceType == 'gain'">
                  <div class="vx-col w-1/2 mt-12">
                    <h5>Recipient</h5>
                    <div class="invoice__recipient-info my-4">
                        <p>{{ invoice_data.citizenName }}</p>

                    </div>
                    <div class="invoice__recipient-contact ">
                        <p class="flex items-center">
                            <feather-icon icon="MailIcon" svgClasses="h-4 w-4"></feather-icon>
                            <span class="ml-2">{{ getActiveUser.email }}</span>
                        </p>
                    </div>
                  </div>
                  <div class="vx-col w-1/2 mt-base text-right mt-12">
                      <h5>{{ invoice_data.serviceName }}</h5>
                      <div class="invoice__company-contact">
                          <p class="mt-4 flex items-center justify-end">
                              <feather-icon icon="MailIcon" svgClasses="h-4 w-4"></feather-icon>
                              <span class="ml-2">{{ invoice_data.serviceEmail }}</span>
                          </p>
                      </div>
                  </div>
                </template>

                <template v-else-if="invoice_data.invoiceType == 'expense'">
                  <div class="vx-col w-1/2 mt-12">
                    <h5>Recipient</h5>
                    <div class="invoice__recipient-info my-4">
                        <p>{{ invoice_data.serviceName }}</p>

                    </div>
                    <div class="invoice__recipient-contact ">
                        <p class="flex items-center">
                            <feather-icon icon="MailIcon" svgClasses="h-4 w-4"></feather-icon>
                            <span class="ml-2">{{ invoice_data.serviceEmail }}</span>
                        </p>
                    </div>
                  </div>
                  <div class="vx-col w-1/2 mt-base text-right mt-12">
                      <h5>{{ invoice_data.citizenName }}</h5>
                      <div class="invoice__company-contact">
                          <p class="mt-4 flex items-center justify-end">
                              <feather-icon icon="MailIcon" svgClasses="h-4 w-4"></feather-icon>
                              <span class="ml-2">{{ getActiveUser.email }}</span>
                          </p>
                      </div>
                  </div>
                </template>
            </div>

            <!-- INVOICE CONTENT -->
            <div class="p-base">
                <!-- INVOICE SUMMARY TABLE -->
                <vs-table hoverFlat class="w-1/2 ml-auto mt-4" :data="invoice_data">
                    <vs-tr>
                        <vs-th class="pointer-events-none">SUBTOTAL</vs-th>
                        <vs-td>{{ invoice_data.amount }} USD</vs-td>
                    </vs-tr>
                    <vs-tr>
                        <vs-th class="pointer-events-none">DISCOUNT (0.0%)</vs-th>
                        <vs-td>0.0 USD</vs-td>
                    </vs-tr>
                    <vs-tr>
                        <vs-th class="pointer-events-none">TOTAL</vs-th>
                        <vs-td>{{ invoice_data.amount }} USD</vs-td>
                    </vs-tr>
                </vs-table>
            </div>

            <!-- INVOICE FOOTER -->
            <div class="invoice__footer text-right p-base">
                <p>
                    <span class="mr-8">BANK: <span class="font-semibold">FTSBUS33</span></span>
                    <span>IBAN: <span class="font-semibold"> G882-1111-2222-3333 </span></span>
                </p>
            </div>
        </vx-card>
    </div>
  </div>
    
    
</template>

<script>
import modulePayment from '@/store/payment/modulePayment.js'

export default{
  data () {
    return {
      invoice_data: null,
      invoice_not_found: false,

      mailTo: ''
    }
  },
  computed: {
    getActiveUser () {
      return this.$store.state.AppActiveUser
    }
  },
  methods: {
    printInvoice () {
      window.print()
    }
  },
  components: {},
  mounted () {
    this.$emit('setAppClasses', 'invoice-page')
  },
  created () {
    // Register Module Payment Module
    if (!modulePayment.isRegistered) {
      this.$store.registerModule('payment', modulePayment)
      modulePayment.isRegistered = true
    }

    const invoiceId = this.$route.params.invoiceId
    this.$store.dispatch('payment/fetchInvoice', invoiceId)
      .then(res => { this.invoice_data = res.data
      console.log(this.invoice_data) })
      .catch(err => {
        if (err.response.status === 404) {
          this.invoice_not_found = true
          return
        }
        console.error(err) 
      })
  }
}
</script>

<style lang="scss">
@media print {
  .invoice-page {
    * {
      visibility: hidden;
    }

    #content-area {
      margin: 0 !important;
    }

    .vs-con-table {
      .vs-con-tbody {
        overflow: hidden !important;
      }
    }

    #invoice-container,
    #invoice-container * {
      visibility: visible;
    }
    #invoice-container {
      position: absolute;
      left: 0;
      top: 0;
      box-shadow: none;
    }
  }
}

@page {
  size: auto;
}
</style>
