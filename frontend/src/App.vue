<template>
  <ManageMeal />
</template>

<script>
import ManageMeal from './components/ManageMeal.vue'
export default {
  name: 'App',
  components: {
    ManageMeal,
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}

img{
  width: 100%;
  height: 200px;
}
</style>
