<template>
  <div id="data-list-list-view" class="data-list-container">

    <service-view-sidebar :isSidebarActive="addNewDataSidebar" @closeSidebar="toggleDataSidebar" :data="sidebarData" />

    <div id="page-service-list">


    <div class="vx-card p-6">
      <!-- <vs-popup classContent="popup-example" title="Give Benefits to the Homeless" active.sync="false">
        <div class="mb-5">
        </div>
        <vs-button vs-button class="mt-4" @click.prevent="" color="primary" type="filled">Submit</vs-button>
      </vs-popup> -->

      <div class="flex flex-wrap items-center">
        
        <!-- ITEMS PER PAGE -->
        <div class="flex-grow">
          <vs-dropdown vs-trigger-click class="cursor-pointer">
            <div class="p-4 border border-solid d-theme-border-grey-light rounded-full d-theme-dark-bg cursor-pointer flex items-center justify-between font-medium">
              <span class="mr-2">{{ currentPage * paginationPageSize - (paginationPageSize - 1) }} - {{ servicesData.length - currentPage * paginationPageSize > 0 ? currentPage * paginationPageSize : servicesData.length }} of {{ servicesData.length }}</span>
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
            </vs-dropdown-menu>
          </vs-dropdown>
          <!-- ADD NEW -->
          <div class="ml-3 p-2 shadow-drop rounded-lg d-theme-dark-light-bg cursor-pointer flex items-end justify-center text-md font-medium w-32 border border-solid border-primary" @click="addNewData">
            <feather-icon icon="PlusIcon" svgClasses="h-6 w-4" />
            <span class="ml-1 mr-2 text-base text-primary">Add New</span>
          </div>
      </div>


      <!-- AgGrid Table -->
      <ag-grid-vue
        ref="agGridTable"
        :components="components"
        :gridOptions="gridOptions"
        class="ag-theme-material w-100 my-4 ag-grid-table"
        :columnDefs="columnDefs"
        :defaultColDef="defaultColDef"
        :rowData="servicesData"
        rowSelection="multiple"
        colResizeDefault="shift"
        :animateRows="true"
        :floatingFilter="false"
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
  </div>
</template>

<script>
import { AgGridVue } from 'ag-grid-vue'
import '@/assets/scss/vuexy/extraComponents/agGridStyleOverride.scss'
import vSelect from 'vue-select'
import ServiceViewSidebar from './ServiceViewSidebar.vue'

// Store Module
import modulePayment from '@/store/payment/modulePayment.js'

// Cell Renderer
import CellRendererActions from './cell-renderer/CellRendererActions.vue'

export default {
  components: {
    AgGridVue,
    vSelect,
    ServiceViewSidebar,
    // Cell Renderer
    CellRendererActions
  },
  data () {
    return {
      // Filter Options
      searchQuery: '',

      // SideBar
      addNewDataSidebar: false,
      sidebarData: {},

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
          width: 125,
          filter: true,
          checkboxSelection: true,
          headerCheckboxSelectionFilteredOnly: true,
          headerCheckboxSelection: true
        },
        {
          headerName: 'Name',
          field: 'name',
          filter: true,
          width: 300
        },
        {
          headerName: 'Email',
          field: 'email',
          filter: true,
          width: 300
        },
        {
          headerName: 'Date Of Registration',
          field: 'registrationDate',
          filter: true,
          width: 300
        },
        {
          headerName: 'Actions',
          field: 'transactions',
          width: 300,
          cellRendererFramework: 'CellRendererActions',
          cellRendererParams : {
            action : this.updateData.bind(this)
          }
        }
      ],
      
      // Cell Renderer Components
      components: {
        CellRendererActions
      }
    }
  },
  watch: {
  },
  computed: {
    getSelectedServices () {
      return this.gridApi.getSelectedRows()
    },
    servicesData () {
      return this.$store.state.payment.services
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
    updateSearchQuery (val) {
      this.gridApi.setQuickFilter(val)
    },
    addNewData () {
      this.sidebarData = {}
      this.toggleDataSidebar(true)
    },
    updateData (serviceData) {
      this.sidebarData = serviceData
      this.toggleDataSidebar(true)
    },
    toggleDataSidebar (val = false) {
      this.addNewDataSidebar = val
    }
  },
  mounted () {
    this.gridApi = this.gridOptions.api
  },
  created () {
    this.$vs.loading()
    if (!modulePayment.isRegistered) {
      this.$store.registerModule('payment', modulePayment)
      modulePayment.isRegistered = true
    }

    this.$store.dispatch('payment/fetchServices').then(() => { 
      this.$vs.loading.close() 
    })
  },
  beforeDestroy () {
  }
}

</script>

<style lang="scss">
#page-service-list {
  .service-list-filters {
    .vs__actions {
      position: absolute;
      right: 0;
      top: 50%;
      transform: translateY(-58%);
    }
  }
}
#data-list-list-view {
  .vs-con-table {

    /*
      Below media-queries is fix for responsiveness of action buttons
      Note: If you change action buttons or layout of this page, Please remove below style
    */
    @media (max-width: 689px) {
      .vs-table--search {
        margin-left: 0;
        max-width: unset;
        width: 100%;

        .vs-table--search-input {
          width: 100%;
        }
      }
    }

    @media (max-width: 461px) {
      .items-per-page-handler {
        display: none;
      }
    }

    @media (max-width: 341px) {
      .data-list-btn-container {
        width: 100%;

        .dd-actions,
        .btn-add-new {
          width: 100%;
          margin-right: 0 !important;
        }
      }
    }

    .product-name {
      max-width: 23rem;
    }

    .vs-table--header {
      display: flex;
      flex-wrap: wrap;
      margin-left: 1.5rem;
      margin-right: 1.5rem;
      > span {
        display: flex;
        flex-grow: 1;
      }

      .vs-table--search{
        padding-top: 0;

        .vs-table--search-input {
          padding: 0.9rem 2.5rem;
          font-size: 1rem;

          &+i {
            left: 1rem;
          }

          &:focus+i {
            left: 1rem;
          }
        }
      }
    }

    .vs-table {
      border-collapse: separate;
      border-spacing: 0 1.3rem;
      padding: 0 1rem;

      tr{
          box-shadow: 0 4px 20px 0 rgba(0,0,0,.05);
          td{
            padding: 20px;
            &:first-child{
              border-top-left-radius: .5rem;
              border-bottom-left-radius: .5rem;
            }
            &:last-child{
              border-top-right-radius: .5rem;
              border-bottom-right-radius: .5rem;
            }
          }
          td.td-check{
            padding: 20px !important;
          }
      }
    }

    .vs-table--thead{
      th {
        padding-top: 0;
        padding-bottom: 0;

        .vs-table-text{
          text-transform: uppercase;
          font-weight: 600;
        }
      }
      th.td-check{
        padding: 0 15px !important;
      }
      tr{
        background: none;
        box-shadow: none;
      }
    }

    .vs-table--pagination {
      justify-content: center;
    }
  }
}
</style>
