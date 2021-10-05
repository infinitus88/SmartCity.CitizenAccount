<!-- =========================================================================================
  File Name: ChartjsComponentRadarChart.vue
  Description: Chartjs component - Radar Chart
  ----------------------------------------------------------------------------------------
  Item Name: Vuexy - Vuejs Styles
  Author: Pixinvent
  Author URL: http://www.themeforest.net/user/pixinvent
========================================================================================== -->


<script>
import { Radar } from 'vue-chartjs'

export default {
  extends: Radar,
  props: {
    data: {
      type: Object,
      default: null
    },
    options: {
      type: Object,
      default: null
    }
  },
  mounted () {
    this.renderChart(this.data, this.options)
  }
}
</script>
