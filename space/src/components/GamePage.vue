<template>
  <div class="game-container" @mousemove="handleMouseMove">
    <Spaceship :zIndex="spaceshipZIndex" :position="spaceshipPosition" />
    <AsteroidField :spaceshipZIndex="spaceshipZIndex" :starsFallSpeed="starsFallSpeed" />
  </div>
</template>

<script lang="ts">
import Spaceship from './Spaceship.vue';
import AsteroidField from './AsteroidField.vue';
import { defineComponent, ref } from 'vue';

export default defineComponent({
  name: 'GamePage',
  components: {
    Spaceship,
    AsteroidField,
  },
  data() {
    return {
      spaceshipZIndex: 100,
      starsFallSpeed: 0.2,
      spaceshipPosition: { x: 0, y: 0 },
    };
  },
  methods: {
    handleMouseMove(event: MouseEvent) {
      this.spaceshipPosition = {
        x: event.clientX,
        y: event.clientY,
      };
    },
  },
});
</script>

<style scoped>
.game-container {
  position: relative;
  width: 100%;
  height: 100vh;
}
</style>