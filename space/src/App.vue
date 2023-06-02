<template>
  <div id="app">
    <SpaceBackground/>
    <router-view/>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useRouter } from 'vue-router';
import SpaceBackground from '@/components/SpaceBackground.vue';

export default defineComponent({
  name: 'App',
  components: {
    SpaceBackground,
  },
  computed: {
  },
});
</script>