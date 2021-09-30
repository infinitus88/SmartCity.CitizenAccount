export default [
  // {
  //   url: "/apps/email",
  //   name: "Email",
  //   slug: "email",
  //   icon: "MailIcon",
  //   i18n: "Email",
  // },
  {
    header: 'Apps',
    icon: 'PackageIcon',
    i18n: 'Apps',
    items: [
      {
        url: '/apps/email',
        name: 'Email',
        slug: 'email',
        icon: 'MailIcon',
        i18n: 'Email'
      },
      {
        url: '/apps/chat',
        name: 'Chat',
        slug: 'chat',
        icon: 'MessageSquareIcon',
        i18n: 'Chat'
      },
      {
        url: '/apps/invoice',
        name: 'Invoice',
        slug: 'invoice',
        icon: 'CreditCardIcon',
        i18n: 'Invoice'
      }
    ]
  },
  {
    header: 'Administrating',
    icon: 'PackageIcon',
    items: [
      {
        url: null,
        name: 'User',
        icon: 'UserIcon',
        i18n: 'User',
        rule: 'admin',
        submenu: [
          {
            url: '/apps/user/user-list',
            name: 'List',
            slug: 'app-user-list',
            i18n: 'List'
          },
          {
            url: '/apps/user/verif-request-list',
            name: 'Request List',
            slug: 'app-verificaton-request-list'
          }
        ]
      },
      {
        url: null,
        name: 'Citizen',
        icon: 'UserCheckIcon',
        i18n: 'Citizen',
        rule: 'admin',
        submenu: [
          {
            url: '/apps/citizen/citizen-list',
            name: 'List',
            slug: 'app-citizen-list',
            i18n: 'List'
          }
        ]
      }
    ]
  }
]

