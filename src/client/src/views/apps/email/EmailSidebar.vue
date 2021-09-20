<!-- =========================================================================================
    File Name: EmailSidebar.vue
    Description: Email Application Sidebar(Filter) (Inbox)
    ----------------------------------------------------------------------------------------
    Item Name: Vuexy - Vuejs, HTML & Laravel Admin Dashboard Template
      Author: Pixinvent
    Author URL: http://www.themeforest.net/user/pixinvent
========================================================================================== -->


<template>
    <div class="email__email-sidebar h-full">
        <div class="m-6 clearfix">
            <vs-button class="bg-primary-gradient w-full" icon-pack="feather" icon="icon-edit" @click="activePrompt = true">Compose</vs-button>
        </div>

        <!-- compose email -->
        <vs-prompt
            class="email-compose"
            title="New Message"
            accept-text= "Send"
            @cancel="clearFields"
            @accept="sendMail"
            @close="clearFields"
            :is-valid="validateForm"
            :active.sync="activePrompt">
                <component :is="scrollbarTag" class="scroll-area p-4" :settings="settings" :key="$vs.rtl">
                    <form @submit.prevent>
                        <vs-input v-validate="'required|email'" name="recieverEmail" label-placeholder="To" v-model="recieverEmail" class="w-full mb-6" :danger="!validateForm && recieverEmail != ''" val-icon-danger="clear" :success="validateForm" val-icon-success="done" :color="validateForm ? 'success' : 'danger'" />
                        <vs-input name="mailSubject" label-placeholder="Subject" v-model="mailSubject" class="w-full mb-6" />
                        <quill-editor v-model="mailMessage" :options="editorOption" />
                    </form>
                </component>
        </vs-prompt>

        <component :is="scrollbarTag" class="email-filter-scroll-area" :settings="settings" :key="$vs.rtl">
            <div class="px-6 pb-2 flex flex-col">

                <!-- inbox -->
                <router-link tag="span" :to="`${baseUrl}/inbox`" class="flex justify-between items-center cursor-pointer" :class="{'text-primary': mailFilter == 'inbox'}">
                    <div class="flex items-center mb-2">
                        <feather-icon icon="MailIcon" :svgClasses="[{'text-primary stroke-current': mailFilter == 'inbox'}, 'h-6 w-6']"></feather-icon>
                        <span class="text-lg ml-3">Inbox</span>
                    </div>
                    <template v-if="emailMeta">
                      <vs-chip class="number" color="primary" v-if="emailMeta.folder.inbox > 0">{{ emailMeta.folder.inbox }}</vs-chip>
                    </template>
                </router-link>

                <!-- sent -->
                <router-link tag="span" :to="`${baseUrl}/sent`" class="flex items-center mt-4 mb-2 cursor-pointer" :class="{'text-primary': mailFilter == 'sent'}">
                    <feather-icon icon="SendIcon" :svgClasses="[{'text-primary stroke-current': mailFilter == 'sent'}, 'h-6 w-6']"></feather-icon>
                    <span class="text-lg ml-3">Sent</span>
                </router-link>

                <!-- starred -->
                <router-link tag="span" :to="`${baseUrl}/starred`" class="flex items-center mt-4 mb-2 cursor-pointer" :class="{'text-primary': mailFilter == 'starred'}">
                    <feather-icon icon="StarIcon" :svgClasses="[{'text-primary stroke-current': mailFilter == 'starred'}, 'h-6 w-6']"></feather-icon>
                    <span class="text-lg ml-3">Starred</span>
                </router-link>

                <!-- trash -->
                <router-link tag="span" :to="`${baseUrl}/trash`" class="flex items-center mt-4 mb-2 cursor-pointer" :class="{'text-primary': mailFilter == 'trash'}">
                    <feather-icon icon="TrashIcon" :svgClasses="[{'text-primary stroke-current': mailFilter == 'trash'}, 'h-6 w-6']"></feather-icon>
                    <span class="text-lg ml-3">Trash</span>
                </router-link>
            </div>
        </component>
    </div>
</template>

<script>
import 'quill/dist/quill.core.css'
import 'quill/dist/quill.snow.css'
import { quillEditor } from 'vue-quill-editor'
import VuePerfectScrollbar from 'vue-perfect-scrollbar'

export default {
  props: {
    mailFilter: {
      type: String,
      required: true
    }
  },
  data () {
    return {
      activePrompt: false,
      recieverEmail: '',
      mailSubject: '',
      mailMessage: '',
      editorOption: {
        modules: {
          toolbar: [
            ['bold', 'italic', 'underline', 'strike', 'link', 'blockquote', 'code-block'],
            [{ 'header': 1 }, { 'header': 2 }],
            [{ 'list': 'ordered' }, { 'list': 'bullet' }],
            [{ 'font': [] }]
          ]
        },
        placeholder: 'Message'
      },
      settings: {
        maxScrollbarLength: 60,
        wheelSpeed: 0.30
      }
    }
  },
  computed: {
    validateForm () {
      return !this.errors.any() && this.recieverEmail !== ''
    },
    baseUrl () {
      const path = this.$route.path
      return path.slice(0, path.lastIndexOf('/'))
    },
    emailMeta () {
      return this.$store.state.email.meta
    },
    scrollbarTag () { return this.$store.getters.scrollbarTag }
  },
  methods: {
    clearFields () {
      this.$nextTick(() => {
        this.recieverEmail = ''
        this.mailSubject = ''
        this.mailMessage = ''
      })
    },
    sendMail () {
      const payload = {
        recieverEmail: this.recieverEmail,
        mailSubject: this.mailSubject,
        mailMessage: this.mailMessage
      }
      this.$store.dispatch('email/sendEmail', payload)

      this.clearFields()
    }
  },
  components: {
    quillEditor,
    VuePerfectScrollbar
  }
}

</script>

