import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  scrollBehavior () {
    return { x: 0, y: 0 }
  },
  routes: [

    {
    // =============================================================================
    // MAIN LAYOUT ROUTES
    // =============================================================================
      path: '',
      component: () => import('./layouts/main/Main.vue'),
      children: [
        // =============================================================================
        // Home Dashboard
        // =============================================================================
        {
          path: '/',
          name: 'home',
          component: () => import('./views/Home.vue'),
          meta: {
            rule: 'user',
            no_scroll: true
          }
        },
        // =============================================================================
        // Application Routes
        // =============================================================================
        {
          path: '/apps/chat',
          name: 'chat',
          component: () => import('./views/apps/chat/Chat.vue'),
          meta: {
            rule: 'user',
            no_scroll: true
          }
        },
        {
          path: '/apps/email',
          redirect: '/apps/email/inbox',
          name: 'email'
        },
        {
          path: '/apps/email/:filter',
          component: () => import('./views/apps/email/Email.vue'),
          meta: {
            rule: 'user',
            parent: 'email',
            no_scroll: true
          }
        },
        {
          path: '/apps/invoice',
          name: 'invoice-list',
          component: () => import('./views/apps/invoice/invoice-list/InvoiceList.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Invoice'},
              { title: 'List', active: true }
            ],
            pageTitle: 'Invoice',
            rule: 'user',
            no_scroll: true
          }
        },
        {
          path: '/apps/invoice/:invoiceId',
          name: 'invoice-view',
          component: () => import('./views/apps/invoice/InvoiceView.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Invoice', url: '/apps/invoice' },
              { title: 'View', active: true }
            ],
            pageTitle: 'Invoice View',
            rule: 'user'
          }
        },
        /*
                  Below route is for demo purpose
                  You can use this route in your app
                    {
                        path: '/apps/eCommerce/item/',
                        name: 'ecommerce-item-detail-view',
                        redirect: '/apps/eCommerce/shop',
                    }
                */
        {
          path: '/apps/payment/service-list',
          name: 'app-service-list',
          component: () => import('@/views/apps/payment/service-list/ServiceList.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Payment' },
              { title: 'List', active: true }
            ],
            pageTitle: 'Service List',
            rule: 'admin'
          }
        },
        {
          path: '/apps/user/user-list',
          name: 'app-user-list',
          component: () => import('@/views/apps/user/user-list/UserList.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'User', url: '/apps/user/user-list' },
              { title: 'List', active: true }
            ],
            pageTitle: 'User List',
            rule: 'admin'
          }
        },
        {
          path: '/apps/user/user-view/:userId',
          name: 'app-user-view',
          component: () => import('@/views/apps/user/UserView.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'User', url: '/apps/user/user-list' },
              { title: 'View', active: true }
            ],
            pageTitle: 'User View',
            rule: 'admin'
          }
        },
        {
          path: '/apps/user/user-edit/:userId',
          name: 'app-user-edit',
          component: () => import('@/views/apps/user/user-edit/UserEdit.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'User', url: '/apps/user/user-list' },
              { title: 'Edit', active: true }
            ],
            pageTitle: 'User Edit',
            rule: 'admin'
          }
        },
        {
          path: '/apps/user/verif-request-list',
          name: 'app-verification-request-list',
          component: () => import('@/views/apps/user/verif-request-list/VerificationRequestList.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'User', url: '/apps/user/user-list' },
              { title: 'Verification Request List', active: true }
            ],
            pageTitle: 'Verification Request List',
            rule: 'admin'
          }
        },

        {
          path: '/apps/citizen/citizen-list',
          name: 'app-citizen-list',
          component: () => import('@/views/apps/citizen/citizen-list/CitizenList.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Citizen', url: '/apps/citizen/citizen-list' },
              { title: 'List', active: true }
            ],
            pageTitle: 'Citizen List',
            rule: 'admin'
          }
        },

        // =============================================================================
        // Pages Routes
        // =============================================================================
        {
          path: '/pages/user-settings',
          name: 'page-user-settings',
          component: () => import('@/views/pages/user-settings/UserSettings.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'User Settings', active: true }
            ],
            pageTitle: 'Settings',
            rule: 'user'
          }
        }
      ]
    },
    // =============================================================================
    // FULL PAGE LAYOUTS
    // =============================================================================
    {
      path: '',
      component: () => import('@/layouts/full-page/FullPage.vue'),
      children: [
        // =============================================================================
        // PAGES
        // =============================================================================
        {
          path: '/payment/request',
          name: 'payment-request-form',
          component: () => import('@/views/pages/payment/PaymentRequestForm.vue'),
          meta: {
            rule: 'user'
          }
        },
        {
          path: '/pages/login',
          name: 'page-login',
          component: () => import('@/views/pages/login/Login.vue'),
          meta: {
            rule: 'user'
          }
        },
        {
          path: '/pages/register',
          name: 'page-register',
          component: () => import('@/views/pages/register/Register.vue'),
          meta: {
            rule: 'user'
          }
        },
        {
          path: '/pages/error-404',
          name: 'page-error-404',
          component: () => import('@/views/pages/Error404.vue'),
          meta: {
            rule: 'user'
          }
        },
        {
          path: '/pages/error-500',
          name: 'page-error-500',
          component: () => import('@/views/pages/Error500.vue'),
          meta: {
            rule: 'user'
          }
        },
        {
          path: '/pages/not-authorized',
          name: 'page-not-authorized',
          component: () => import('@/views/pages/NotAuthorized.vue'),
          meta: {
            rule: 'editor'
          }
        }
      ]
    },
    // Redirect to 404 page, if no match found
    {
      path: '*',
      redirect: '/pages/error-404'
    }
  ]
})

router.afterEach(() => {
  // Remove initial loading
  const appLoading = document.getElementById('loading-bg')
  if (appLoading) {
    appLoading.style.display = 'none'
  }
})

export default router
