<template functional>
    <footer class="the-footer flex-wrap justify-between" :class="classes">
        <p>
          <span>COPYRIGHT &copy;</span>
          <span>{{ new Date().getFullYear() }} </span>
          <a href="https://github.com/infinitus88" target="_blank" rel="nofollow">Citizen Account</a>
          <span class="hidden sm:inline-block">, All rights Reserved</span>
        </p>
    </footer>
</template>

<script>
export default {
  name: 'the-footer',
  props: {
    classes: {
      type: String
    }
  }
}
</script>
