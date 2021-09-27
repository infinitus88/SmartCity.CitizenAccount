<template>
    <div :style="{'direction': $vs.rtl ? 'rtl' : 'ltr'}">
      <vs-button 
        color="success"
        class="mr-4" 
        size="default" 
        type="filled"
        @click="acceptRequest"
        >
        Accept
      </vs-button>
      <vs-button 
        color="danger"
        class="mr-4" 
        size="default" 
        type="filled"
        >
        Reject
      </vs-button>
    </div>
</template>

<script>
export default {
  name: 'CellRendererActions',
  methods: {
    rejectRequest () {
      const payload = {
        status: 'rejected'
      }
      this.$store.dispatch('userManagement/updateVerificationRequest', this.params.data.id, payload)
        .then(()   => {
          this.showAcceptSuccess() 
        })
        .catch(err => { console.error(err) })
    },
    acceptRequest () {
      const payload = {
        id: this.params.data.id,
        status: 'accepted'
      }
      this.$store.dispatch('userManagement/updateVerificationRequest', payload)
        .then(()   => {
          this.showAcceptSuccess() 
        })
        .catch(err => { console.error(err) })
    },
    showAcceptSuccess () {
      this.$vs.notify({
        color: 'success',
        title: 'Request Accepted',
        text: 'The selected request was successfully accepted'
      })
    },
    showRejectSuccess () {
      this.$vs.notify({
        color: 'success',
        title: 'Request Rejected',
        text: 'The selected request was successfully rejected'
      })
    }
  }
}
</script>
