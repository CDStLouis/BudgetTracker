<script setup lang="ts">
import HelloWorld from './components/HelloWorld.vue'

fetch("https://localhost:5156/weatherforecast")
  .then(res => res.json())
  .then(data => console.log(data))
</script>

<template>
  <HelloWorld />
</template>
