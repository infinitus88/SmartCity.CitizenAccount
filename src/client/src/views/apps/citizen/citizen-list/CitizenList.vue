<template>

  <div id="page-citizen-list">

    <vx-card ref="filterCard" title="Filters" class="citizen-list-filters mb-8" actionButtons @refresh="resetColFilters" @remove="resetColFilters">
      <div class="vx-row">
        <div class="vx-col md:w-1/4 sm:w-1/2 w-full">
          <label class="text-sm opacity-75">Gender</label>
          <v-select :options="genderOptions" :clearable="false" :dir="$vs.rtl ? 'rtl' : 'ltr'" v-model="genderFilter" class="mb-4 md:mb-0" />
        </div>
      </div>
    </vx-card>

    <div class="vx-card p-6">

      <vs-popup classContent="popup-example" title="Give Benefits to the Homeless" :active.sync="benefitsPopupActive">
        <div class="mb-5">
          <label class="vs-input--label">Amount</label>
          <vs-input
            icon-pack="feather"
            icon="icon-dollar-sign"
            v-model="benefitsAmount"
            class="w-full"
            v-validate="{ required: true, regex: /\d+(\.\d+)?$/ }"
            name="amount" 
          />
          <span class="text-danger text-sm" v-show="errors.has('amount')">{{ errors.first('amount') }}</span>
        </div>
        <vs-button vs-button class="mt-4" @click="giveBenefits" color="primary" type="filled">Submit</vs-button>
      </vs-popup>

      <div class="flex flex-wrap items-center">
        
        <!-- ITEMS PER PAGE -->
        <div class="flex-grow">
          <vs-dropdown vs-trigger-click class="cursor-pointer">
            <div class="p-4 border border-solid d-theme-border-grey-light rounded-full d-theme-dark-bg cursor-pointer flex items-center justify-between font-medium">
              <span class="mr-2">{{ currentPage * paginationPageSize - (paginationPageSize - 1) }} - {{ citizensData.length - currentPage * paginationPageSize > 0 ? currentPage * paginationPageSize : citizensData.length }} of {{ citizensData.length }}</span>
              <feather-icon icon="ChevronDownIcon" svgClasses="h-4 w-4" />
            </div>
            <!-- <vs-button class="btn-drop" type="line" color="primary" icon-pack="feather" icon="icon-chevron-down"></vs-button> -->
            <vs-dropdown-menu>

              <vs-dropdown-item @click="gridApi.paginationSetPageSize(10)">
                <span>10</span>
              </vs-dropdown-item>
              <vs-dropdown-item @click="gridApi.paginationSetPageSize(20)">
                <span>20</span>
              </vs-dropdown-item>
              <vs-dropdown-item @click="gridApi.paginationSetPageSize(25)">
                <span>25</span>
              </vs-dropdown-item>
              <vs-dropdown-item @click="gridApi.paginationSetPageSize(30)">
                <span>30</span>
              </vs-dropdown-item>
            </vs-dropdown-menu>
          </vs-dropdown>
        </div>

        <!-- TABLE ACTION COL-2: SEARCH & EXPORT AS CSV -->
          <vs-input class="sm:mr-4 mr-0 sm:w-auto w-full sm:order-normal order-3 sm:mt-0 mt-4" v-model="searchQuery" @input="updateSearchQuery" placeholder="Search..." />
          <!-- <vs-button class="mb-4 md:mb-0" @click="gridApi.exportDataAsCsv()">Export as CSV</vs-button> -->

          <!-- ACTION - DROPDOWN -->
          <vs-dropdown vs-trigger-click class="cursor-pointer">
            <div class="p-3 shadow-drop rounded-lg d-theme-dark-light-bg cursor-pointer flex items-end justify-center text-lg font-medium w-32">
              <span class="mr-2 leading-none">Actions</span>
              <feather-icon icon="ChevronDownIcon" svgClasses="h-4 w-4" />
            </div>
            <vs-dropdown-menu>
              <vs-dropdown-item>
                <span class="flex items-center">
                  <feather-icon icon="TrashIcon" svgClasses="h-4 w-4" class="mr-2" />
                  <span>Delete</span>
                </span>
              </vs-dropdown-item>
              <vs-dropdown-item>
                <span class="flex items-center">
                  <feather-icon icon="DollarSignIcon" svgClasses="h-4 w-4" class="mr-2" />
                  <span @click="benefitsPopupActive=true">GiveBenefits</span>
                </span>
              </vs-dropdown-item>
            </vs-dropdown-menu>
          </vs-dropdown>
      </div>


      <!-- AgGrid Table -->
      <ag-grid-vue
        ref="agGridTable"
        :components="components"
        :gridOptions="gridOptions"
        class="ag-theme-material w-100 my-4 ag-grid-table"
        :columnDefs="columnDefs"
        :defaultColDef="defaultColDef"
        :rowData="citizensData"
        rowSelection="multiple"
        colResizeDefault="shift"
        :animateRows="true"
        :floatingFilter="true"
        :pagination="true"
        :paginationPageSize="paginationPageSize"
        :suppressPaginationPanel="true"
        :enableRtl="$vs.rtl">
      </ag-grid-vue>

      <vs-pagination
        :total="totalPages"
        :max="7"
        v-model="currentPage" />

    </div>
  </div>

</template>

<script>
import { AgGridVue } from 'ag-grid-vue'
import '@/assets/scss/vuexy/extraComponents/agGridStyleOverride.scss'
import vSelect from 'vue-select'

// Store Module
import moduleCitizen from '@/store/citizen/moduleCitizen.js'
import modulePayment from '@/store/payment/modulePayment.js'

// Cell Renderer
import CellRendererActions from './cell-renderer/CellRendererActions.vue'
import CellRendererBalance from './cell-renderer/CellRendererBalance.vue'
import CellRendererName from './cell-renderer/CellRendererName.vue'


export default {
  components: {
    AgGridVue,
    vSelect,

    // Cell Renderer
    CellRendererActions,
    CellRendererBalance,
    CellRendererName
  },
  data () {
    return {
      benefitsPopupActive: false,
      benefitsAmount: '',

      genderFilter: { label: 'All', value: 'all'},
      genderOptions: [
        { label: 'All', value: 'all' },
        { label: 'Male', value: 'male' },
        { label: 'Female', value: 'female' },
        { label: 'Other', value: 'other' }
      ],

      // Filter Options
      searchQuery: '',

      // AgGrid
      gridApi: null,
      gridOptions: {},
      defaultColDef: {
        sortable: true,
        resizable: true,
        suppressMenu: true
      },
      columnDefs: [
        {
          headerName: 'ID',
          field: 'id',
          width: 415,
          filter: true,
          checkboxSelection: true,
          headerCheckboxSelectionFilteredOnly: true,
          headerCheckboxSelection: true
        },
        {
          headerName: 'Name',
          field: 'fullName',
          filter: true,
          width: 250,
          cellRendererFramework: 'CellRendererName'
        },
        {
          headerName: 'Gender',
          field: 'gender',
          filter: true,
          width: 135
        },
        {
          headerName: 'Balance',
          field: 'balance',
          filter: true,
          width: 150,
          cellRendererFramework: 'CellRendererBalance'
        },
        {
          headerName: 'Date Of Birth',
          field: 'dateOfBirth',
          filter: true,
          width: 200
        },
        {
          headerName: 'Date Of Registration',
          field: 'registrationDate',
          filter: true,
          width: 200
        },
        {
          headerName: 'Actions',
          field: 'transactions',
          width: 150,
          cellRendererFramework: 'CellRendererActions'
        }
      ],

      // Cell Renderer Components
      components: {
        CellRendererActions,
        CellRendererBalance,
        CellRendererName
      }
    }
  },
  watch: {
    genderFilter (obj) {
      this.setColumnFilter('gender', obj.value)
    }
  },
  computed: {
    getSelectedCitizens () {
      return this.gridApi.getSelectedRows()
    },
    citizensData () {
      return this.$store.state.citizen.citizens
    },
    paginationPageSize () {
      if (this.gridApi) return this.gridApi.paginationGetPageSize()
      else return 10
    },
    totalPages () {
      if (this.gridApi) return this.gridApi.paginationGetTotalPages()
      else return 0
    },
    currentPage: {
      get () {
        if (this.gridApi) return this.gridApi.paginationGetCurrentPage() + 1
        else return 1
      },
      set (val) {
        this.gridApi.paginationGoToPage(val - 1)
      }
    }
  },
  methods: {
    giveBenefits () {
      console.log('giveBenenfits')
      const payload = {
        citizenIds: this.getSelectedCitizens.map(c => c.id),
        amount: parseInt(this.benefitsAmount),
        serviceName: 'Citizen Account'
      }

      this.$store.dispatch('payment/giveBenefits', payload)
        .then(() => {
          this.$store.dispatch('citizen/fetchCitizens').catch(err => { console.error(err) })
          this.gridApi.refreshCells()
          this.benefitsAmount = ''
          this.benefitsPopupActive = false 
        })
        .catch(err => { console.error(err) })
      // this.benefitsPopupActive = false
      
    },
    setColumnFilter (column, val) {
      const filter = this.gridApi.getFilterInstance(column)
      let modelObj = null

      if (val !== 'all') {
        modelObj = { type: 'equals', filter: val }
      }

      filter.setModel(modelObj)
      this.gridApi.onFilterChanged()
    },
    resetColFilters () {
      // Reset Grid Filter
      this.gridApi.setFilterModel(null)
      this.gridApi.onFilterChanged()

      // Reset Filter Options
      this.genderFilter = { label: 'All', value: 'all' }

      this.$refs.filterCard.removeRefreshAnimation()
    },
    updateSearchQuery (val) {
      this.gridApi.setQuickFilter(val)
    }
  },
  mounted () {
    this.gridApi = this.gridOptions.api
  },
  created () {
    this.$store.registerModule('payment', modulePayment)
    if (!moduleCitizen.isRegistered) {
      this.$store.registerModule('citizen', moduleCitizen)
      moduleCitizen.isRegistered = true
    }
    this.$store.dispatch('citizen/fetchCitizens')
  }
}

</script>

<style lang="scss">
#page-citizen-list {
  .citizen-list-filters {
    .vs__actions {
      position: absolute;
      right: 0;
      top: 50%;
      transform: translateY(-58%);
    }
  }
}
</style>
