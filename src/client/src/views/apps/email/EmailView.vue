<template>
    <div>
        <vs-sidebar click-not-close parent="#email-app" :hidden-background="true" class="full-vs-sidebar email-view-sidebar items-no-padding" v-model="isSidebarActive" position-right>
            <div class="mail-sidebar-content px-0 sm:pb-6 h-full" v-if="currentMail">
                <div class="flex flex-wrap items-center email-header justify-between md:px-8 px-6 sm:pb-2 sm: pt-6 d-theme-dark-bg">
                    <div class="flex mb-4">
                        <div class="flex items-center">
                            <feather-icon :icon="$vs.rtl ? 'ArrowRightIcon' : 'ArrowLeftIcon'" @click="$emit('closeSidebar')" class="cursor-pointer mr-4" svg-classes="w-6 h-6"></feather-icon>
                            <h4 v-if="currentMail.subject">{{ currentMail.subject }}</h4>
                            <h4 v-else>(no subject)</h4>
                        </div>
                    </div>
                    <div class="ml-10 mb-4 mt-1">
                        <div class="email__actions--single flex items-baseline">
                            <feather-icon icon="StarIcon" class="cursor-pointer" :svgClasses="[{'text-warning stroke-current stroke-current': currentMail.isStarred}, 'h-6 w-6']" @click="toggleIsStarred" />

                            <feather-icon icon="MailIcon" svg-classes="h-6 w-6" class="cursor-pointer ml-4" @click="$emit('markUnread')"></feather-icon>
                            <feather-icon v-if="currentMail.mailFolder != 'trash'" icon="TrashIcon" svg-classes="h-6 w-6" class="cursor-pointer ml-4" @click="$emit('removeMail')"></feather-icon>
                        </div>
                    </div>
                </div>

                <component :is="scrollbarTag" class="scroll-area-mail-content md:px-8 md:pb-8 pt-4 px-6 pb-6" :settings="settings" :key="$vs.rtl">
                  <div v-if="isSidebarActive">
                    <!-- LATEST MESSAGE -->
                    <div class="vx-row">
                        <div class="vx-col w-full">
                          <email-mail-card :mailContent="currentMail" />
                        </div>
                    </div>
                  </div>
                </component>
            </div>
        </vs-sidebar>
    </div>
</template>

<script>
import VuePerfectScrollbar from 'vue-perfect-scrollbar'
import EmailMailCard from './EmailMailCard.vue'

export default {
  props: {
    openMailId: {
      required: true,
      validator: prop => typeof prop === 'number' || prop === null
    },
    isSidebarActive: {
      type: Boolean,
      required: true
    },
    mailFilter: {
      type: String
    }
  },
  data () {
    return {
      showThread: false,
      settings: {
        maxScrollbarLength: 60,
        wheelSpeed: 0.50
      }
    }
  },
  watch: {
    isSidebarActive (value) {
      if (!value) {
        this.$emit('closeSidebar')
        setTimeout(() => {
          this.showThread = false
        }, 500)
      }
    }
  },
  computed: {
    currentMail () {
      return this.$store.getters['email/getMail'](this.openMailId)
    },
  
    scrollbarTag () { return this.$store.getters.scrollbarTag }
  },
  methods: {
    toggleIsStarred () {
      const payload = { mailId: this.openMailId, value: !this.currentMail.isStarred }
      this.$store.dispatch('email/toggleIsMailStarred', payload)
    },
    moveTo (to) {
      this.$emit('closeSidebar')
      this.$emit('moveTo', to)
    }
  },
  components: {
    VuePerfectScrollbar,
    EmailMailCard
  },
  updated () {
    if (!this.currentMail) return
    if (this.currentMail.unread && this.isSidebarActive) this.$store.dispatch('email/setUnread', { emailIds: [this.openMailId], unreadFlag: false })
  }
}

</script>

