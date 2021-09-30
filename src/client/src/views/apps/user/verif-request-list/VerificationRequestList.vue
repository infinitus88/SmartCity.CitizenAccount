<template>

  <div id="page-verif-request-list">

    <div class="vx-card p-6">

      <div class="flex flex-wrap items-center">

        <!-- ITEMS PER PAGE -->
        <div class="flex-grow">
          <vs-dropdown vs-trigger-click class="cursor-pointer">
            <div class="p-4 border border-solid d-theme-border-grey-light rounded-full d-theme-dark-bg cursor-pointer flex items-center justify-between font-medium">
              <span class="mr-2">{{ currentPage * paginationPageSize - (paginationPageSize - 1) }} - {{ requestsData.length - currentPage * paginationPageSize > 0 ? currentPage * paginationPageSize : requestsData.length }} of {{ requestsData.length }}</span>
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
                  <feather-icon icon="CheckCircleIcon" svgClasses="h-4 w-4" class="mr-2" />
                  <span @click="acceptSelected">Accept</span>
                </span>
              </vs-dropdown-item>
              <vs-dropdown-item>
                <span class="flex items-center">
                  <feather-icon icon="XCircleIcon" svgClasses="h-4 w-4" class="mr-2" />
                  <span @click="rejectSelected">Reject</span>
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
        :rowData="requestsData"
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
import moduleUserManagement from '@/store/user-management/moduleUserManagement.js'

// Cell Renderer
import CellRendererActions from './cell-renderer/CellRendererActions.vue'
import CellRendererUserName from './cell-renderer/CellRendererUserName.vue'
import CellRendererCitizenName from './cell-renderer/CellRendererCitizenName.vue'


export default {
  components: {
    AgGridVue,
    vSelect,

    // Cell Renderer
    CellRendererActions,
    CellRendererUserName,
    CellRendererCitizenName
  },
  data () {
    return {
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
          width: 125,
          filter: true,
          checkboxSelection: true,
          headerCheckboxSelectionFilteredOnly: true,
          headerCheckboxSelection: true
        },
        {
          headerName: 'User Name',
          field: 'userData.displayName',
          filter: true,
          width: 350,
          cellRendererFramework: 'CellRendererUserName',
          cellClass: 'text-center'
        },
        {
          headerName: 'Citizen Name',
          field: 'citizenData.fullName',
          filter: true,
          width: 350,
          cellRendererFramework: 'CellRendererCitizenName',
          cellClass: 'text-center'
        },
        {
          headerName: 'Actions',
          field: 'transactions',
          width: 300,
          cellRendererFramework: 'CellRendererActions',
          cellClass: 'text-center'
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
    getSelectedRequests () {
      return this.gridApi.getSelectedRows()
    },
    requestsData () {
      return this.$store.state.userManagement.verificationRequests
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
    acceptSelected () {
      if (this.getSelectedRequests.map(u => u.id).length !== 0) {
        const payload = {
          requestIds: this.getSelectedRequests.map(u => u.id),
          status: 'accepted'
        }

        this.$store.dispatch('userManagement/updateVerificationRequests', payload).catch((err) => { console.log(err) })    
      }
    },
    rejectSelected () {
      if (this.getSelectedRequests.map(u => u.id).length !== 0) {
        const payload = {
          requestIds: this.getSelectedRequests.map(u => u.id),
          status: 'rejected'
        }

        this.$store.dispatch('userManagement/updateVerificationRequests', payload).catch((err) => { console.log(err) })    
      }
    },
    updateSearchQuery (val) {
      this.gridApi.setQuickFilter(val)
    }
  },
  mounted () {
    this.gridApi = this.gridOptions.api
  },
  created () {
    if (!moduleUserManagement.isRegistered) {
      this.$store.registerModule('userManagement', moduleUserManagement)
      moduleUserManagement.isRegistered = true
    }
    this.$store.dispatch('userManagement/fetchVerificationRequests').catch(err => { console.error(err) })
  }
  
}

</script>

<style lang="scss">
#page-verif-request-list {
  .verif-request-list-filters {
    .vs__actions {
      position: absolute;
      right: 0;
      top: 50%;
      transform: translateY(-58%);
    }
  }
}
</style>
