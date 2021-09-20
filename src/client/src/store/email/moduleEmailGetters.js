export default {
  filteredMails: state => state.mails.filter((mail) => {
    return (state.mail_filter === 'starred' ? mail.isStarred : state.mail_filter === mail.folder) && (mail.displayName.toLowerCase().includes(state.mailSearchQuery.toLowerCase()) || mail.emailAddress.toLowerCase().includes(state.mailSearchQuery.toLowerCase()) || mail.subject.toLowerCase().includes(state.mailSearchQuery.toLowerCase()) || mail.message.toLowerCase().includes(state.mailSearchQuery.toLowerCase()))
  }),
  getMail: state => (emailId) => state.mails.find((email) => email.id === emailId),
  isLoading: state => state.isLoading
}
