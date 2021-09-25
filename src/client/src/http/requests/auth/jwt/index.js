import axios from '../../../axios/index.js'
import store from '../../../../store/store.js'

// Token Refresh
let isAlreadyFetchingAccessToken = false

export default {
  init () {
    axios.interceptors.response.use(function (response) {
      return response
    }, function (error) {
      // const { config, response: { status } } = error
      const { response } = error
  

      // if (status === 401) {
      if (response && response.status === 401) {
        if (!isAlreadyFetchingAccessToken) {
          isAlreadyFetchingAccessToken = true
          store.dispatch('auth/fetchAccessToken')
            .then(() => {
              isAlreadyFetchingAccessToken = false
              // onAccessTokenFetched(access_token)
            })
        }
        return
      }
      return Promise.reject(error)
    })
  },
  login (email, pwd) {
    return axios.post('/api/auth/login', {
      email,
      password: pwd
    })
  },
  registerUser (name, email, pwd) {
    return axios.post('/api/auth/register', {
      displayName: name,
      email,
      password: pwd
    })
  },
  refreshToken () {
    return axios.get('/api/auth/refresh-token', { headers: { 'Authorization': `Bearer ${localStorage.getItem('accessToken')}`} })
  }
}
