export default {
  SET_LOADING (state, status) {
    state.isLoading = status
  },
  SET_EMAIL_SEARCH_QUERY (state, query) {
    state.mailSearchQuery = query
  },
  SET_MAILS (state, mails) {
    state.mails = mails
  },
  SET_META (state, meta) {
    state.meta = meta
  },
  UPDATE_MAIL_FILTER (state, filterName) {
    state.mail_filter = filterName
  },
  SET_UNREAD (state, payload) {
    payload.emailIds.forEach((mailId) => {
      const mailIndex = state.mails.findIndex((mail) => mail.id === mailId)
      if (mailIndex !== -1) state.mails[mailIndex].unread = payload.unreadFlag
    })
  },

  MOVE_MAILS_TO (state, payload) {
    payload.emailIds.forEach((mailId) => {
      const mailIndex = state.mails.findIndex((mail) => mail.id === mailId)

      state.mails[mailIndex].folder = payload.to
    })
  },

  TOGGLE_IS_MAIL_STARRED (state, payload) {
    state.mails.find((mail) => mail.id === payload.mailId).isStarred = payload.value
  },

  // If your process of fetching is different than ours. Please update action and mutation
  // Maybe this mutation is redundant for you. Feel free to remove it.
  // UPDATE_UNREAD_META (state, payload) {
  // },
  UPDATE_UNREAD_META_ON_MOVE (state, payload) {

    // Updating Draft meta is handled by "MOVE_MAILS_TO" mutation

    payload.emailIds.forEach((mailId) => {
      const mail = state.mails.find((mail) => mail.id === mailId)

      if (mail.unread) {
        const cf_unread_mails = state.meta.folder[state.mail_filter]
        if (cf_unread_mails) {
          cf_unread_mails.splice(cf_unread_mails.indexOf(mailId), 1)
        }
        if (state.meta.folder[payload.to]) state.meta.folder[payload.to].push(mailId)
      }
    })
  }
}
