import axios from '@/axios.js'
// import { reject, resolve } from 'core-js/fn/promise'

export default {
  setEmailSearchQuery ({ commit }, query) {
    commit('SET_EMAIL_SEARCH_QUERY', query)
  },

  // Send mail
  // eslint-disable-next-line no-unused-vars
  sendEmail ({ commit }, payload) {
    return new Promise((resolve, reject) => {
      axios.post('/api/email/send-mail', {
        emailAddress: payload.recieverEmail,
        subject: payload.mailSubject,
        message: payload.mailMessage
      }).then((response) => {
        resolve(response)
      })
        .catch((error) => { reject(error) })
    })
  },

  // Fetch emails
  fetchEmails ({ commit }, payload) {
    commit('SET_LOADING', true)
    return new Promise((resolve, reject) => {
      
      axios.get('/api/email/mails', { params: {filter: payload.filter} })
        .then((response) => {

          commit('SET_MAILS', response.data)
          commit('SET_LOADING', false)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  },

  // Fetch Email Meta
  fetchMeta ({ commit }) {
    return new Promise((resolve, reject) => {
      axios.get('/api/email/meta')
        .then((response) => {
          commit('SET_META', response.data)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  },

  // Move mails to another folder
  moveMailsTo ({ commit }, payload) {
    return new Promise((resolve, reject) => {
      axios.post('/api/email/move-mails', { emailIds: payload.emailIds,
        folder: payload.to })
        .then((response) => {
          commit('MOVE_MAILS_TO', payload)
          commit('SET_META', response.data)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  },

  // Set mails flag unread to true
  setUnread ({ commit }, payload) {
    return new Promise((resolve, reject) => {
      axios.post('/api/email/mark-unread', { emailIds: payload.emailIds,
        unreadFlag: payload.unreadFlag })
        .then((response) => {
          commit('SET_UNREAD', payload)
          
          commit('SET_META', response.data)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  },

  // Toggle isStarred flag in mail
  toggleIsMailStarred ({ commit }, payload) {
    return new Promise((resolve, reject) => {
      axios.post('/api/email/set-starred', { mailId: payload.mailId,
        value: payload.value })
        .then((response) => {
          commit('TOGGLE_IS_MAIL_STARRED', payload)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  }
}
