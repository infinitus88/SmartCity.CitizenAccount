import themeConfig, { colors } from '@/../themeConfig.js'

const userDefaults = {
  id         : 0,          // From Auth
  displayName : 'John Doe', // From Auth
  about       : 'Dessert chocolate cake lemon drops jujubes. Biscuit cupcake ice cream bear claw brownie brownie marshmallow.',
  photoUrl    : require('@/assets/images/portrait/small/avatar-s-11.jpg'), // From Auth
  status      : 'active',
  role    : 'user'
}

const userInfoLocalStorage = JSON.parse(localStorage.getItem('userInfo')) || {}

// Set default values for active-user
const getUserInfo = () => {
  const userInfo = {}

  // Update property in user
  Object.keys(userDefaults).forEach((key) => {
    // If property is defined in localStorage => Use that
    userInfo[key] = userInfoLocalStorage[key] ?  userInfoLocalStorage[key] : userDefaults[key]
  })

  // Include properties from localStorage
  Object.keys(userInfoLocalStorage).forEach((key) => {
    if (userInfo[key] === undefined && userInfoLocalStorage[key] !== null) userInfo[key] = userInfoLocalStorage[key]
  })

  return userInfo
}


// Check if device is touch device
const is_touch_device = () => {
  const prefixes = ' -webkit- -moz- -o- -ms- '.split(' ')
  const mq = function (query) {
    return window.matchMedia(query).matches
  }

  if ('ontouchstart' in window || window.DocumentTouch) {
    return true
  }

  // include the 'heartz' as a way to have a non matching MQ to help terminate the join
  // https://git.io/vznFH
  const query = ['(', prefixes.join('touch-enabled),('), 'heartz', ')'].join('')
  return mq(query)
}


// /////////////////////////////////////////////
// State
// /////////////////////////////////////////////

const state = {
  AppActiveUser           : getUserInfo(),
  bodyOverlay             : false,
  isVerticalNavMenuActive : true,
  is_touch_device         : is_touch_device(),
  mainLayoutType          : themeConfig.mainLayoutType,
  reduceButton            : themeConfig.sidebarCollapsed,
  verticalNavMenuWidth    : 'default',
  verticalNavMenuItemsMin : false,
  scrollY                 : 0,
  theme                   : themeConfig.theme,
  themePrimaryColor       : colors.primary,

  // Can be used to get current window with
  // Note: Above breakpoint state is for internal use of sidebar & navbar component
  windowWidth: null
}

export default state
