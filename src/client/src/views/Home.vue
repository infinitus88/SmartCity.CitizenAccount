<template>
  <div id="home-base-card">
    <div class="vx-row">
      <!-- WHEATHER CARD -->
      <div class="vx-col w-full lg:w-1/3 sm:w-1/2 mb-base">
        <vx-card class="overlay-card overflow-hidden">
          <template slot="no-body">
            <img :src="wheatherInfo.overlay_img" alt="user-profile-cover" class="responsive blur-1">
            <div class="card-overlay text-white">
              <div class="flex flex-col justify-between h-full">
                <div class="text-center mt-8 w-full">
                  <h3 class="text-white mb-2 tracking-wide">{{ wheatherInfo.weather }}</h3>
                  <p class="mb-6">{{ wheatherInfo.place_name }}</p>
                  <div class="flex justify-around">
                    <feather-icon :icon="wheatherInfo.weather_icon" svgClasses="w-24 h-24 text-white" />
                    <h2 class="text-white text-big">{{ wheatherInfo.temperature }} <sup class="text-2xl">o</sup> </h2>
                  </div>
                </div>
                <div class="text-center w-full">
                  <div
                    class="flex justify-between px-8 mb-8 text-xl"
                    v-for="meta in wheatherInfo.meta"
                    :key="meta.label">
                    <span>{{ meta.label }}</span>
                    <span>{{ meta.value }}</span>
                  </div>
                </div>
              </div>
            </div>
          </template>
        </vx-card>
      </div>
    </div>
  </div>
</template>
<script>

export default {

  data () {
    return {
      wheatherInfo: {
        overlay_img: require('@/assets/images/pages/card-image-5.jpg'),
        place_name: 'New York',
        weather: 'Snowy',
        weather_icon: 'CloudSnowIcon',
        temperature: -6,
        meta: [
          {
            label: 'Precipitation',
            value: '48%'
          },
          {
            label: 'Humidity',
            value: '60%'
          },
          {
            label: 'Wind',
            value: '4823 km/h'
          }
        ]
      }
    }
  },
  created () {
  }
}
</script>

<style lang="scss">
#home-base-card {
    .overlay-card {
        .vx-card__collapsible-content {
            max-height: 485px;
        }
    }

    .chat-card-log {
        height: 360px;

        .chat-sent-msg {
            background-color: #f2f4f7 !important;
        }
    }

    .card-video {
        .video-js {
            height: 370px;
        }
    }
}
</style>