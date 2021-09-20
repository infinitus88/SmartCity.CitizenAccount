<template>
  <div
    class="
      h-screen
      flex
      w-full
      bg-img
      vx-row
      no-gutter
      items-center
      justify-center
    "
  >
    <div class="vx-col sm:w-1/2 md:w-1/2 lg:w-3/4 xl:w-2/5 sm:m-0 m-4">
      <vx-card>
            <div
              class="
                vx-col
                sm:w-full
                md:w-full
                lg:w-2/2
                mx-auto
                self-center
                d-theme-dark-bg
              "
            >
              <div class="px-8 pt-8 register-tabs-container">
                <div class="vx-card__title mb-4">
                  <h4 class="mb-4">Request Payment</h4>
                  <p>Fill the below form to make a payment.</p>
                </div>

                <div class="clearfix">
                  <v-select name="citizen" class="w-full" v-model="citizenId" :options="options" label="title">
                    <template v-slot:option="option">
                      <div></div>
                      <vs-avatar class="border-2 border-solid border-white" :src="photoUrl" size="40px"></vs-avatar>
                      
                    </template>
                  </v-select>

                  <vs-input
                    v-validate="'required'"
                    type="number"
                    data-vv-validate-on="blur"
                    label-placeholder="Name"
                    name="displayName"
                    placeholder="Name"
                    v-model="displayName"
                    class="w-full"
                  />

                  <vs-input
                    type="password"
                    data-vv-validate-on="blur"
                    name="confirm_password"
                    label-placeholder="Confirm Password"
                    placeholder="Confirm Password"
                    class="w-full mt-6"
                  />

                  <vs-button type="border" to="/pages/login" class="mt-6"
                    >Login</vs-button
                  >
                  <vs-button
                    class="float-right mt-6"
                    >Register</vs-button
                  >
                </div>
              </div>
            </div>
      </vx-card>
    </div>
  </div>
</template>

<script>
import vSelect from 'vue-select'

export default {


  data () {
    return {
      photoUrl: require('@/assets/images/portrait/small/avatar-s-5.jpg'),
      displayName: '',
      citizenId: '',
      options: [
        {
          title: 'Read the Docs',
          icon: 'fa-book',
          url: 'https://codeclimate.com/github/sagalbot/vue-select'
        },
        {
          title: 'View on GitHub',
          icon: 'fa-github',
          url: 'https://codeclimate.com/github/sagalbot/vue-select'
        },
        {
          title: 'View on NPM',
          icon: 'fa-database',
          url: 'https://codeclimate.com/github/sagalbot/vue-select'
        },
        {
          title: 'View Codepen Examples',
          icon: 'fa-pencil',
          url: 'https://codeclimate.com/github/sagalbot/vue-select'
        }
      ]
    }
  },
  components: {
    vSelect
  }
}
</script>

<style lang="scss">
.register-tabs-container {
  min-height: 517px;

  .con-tab {
    padding-bottom: 23px;
  }
}
</style>
