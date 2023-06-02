<template>
  <div class="space-background">
    <div class="stars">
      <div v-for="(star, index) in stars" :key="index" class="star" :style="star.style"></div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, computed, onMounted, onUnmounted } from 'vue';

interface Star {
  x: number;
  y: number;
  speed: number;
  opacity: number;
  style: { [key: string]: string };
}

export default defineComponent({
  name: 'SpaceBackground',
  setup() {
    const stars = ref<Star[]>([]);
    const animateInterval = ref<number | undefined>(undefined);
    const startTime = Date.now();
    const maxSpeed = 5; // Maximum speed of stars
    const accelerationRate = 0.001; // Rate of acceleration

    // Calculate the position, speed, and opacity of a star
    function calculateStarPosition(): Star {
      const x = Math.random() * window.innerWidth;
      const y = Math.random() * window.innerHeight;
      const speed = Math.random() + 1; // Adjust the initial speed as needed
      const opacity = Math.random() * 0.5 + 0.5; // Adjust the opacity as needed
      return { x, y, speed, opacity, style: {} };
    }

    // Create initial stars
    function createStars(): void {
      const count = Math.floor(window.innerWidth / 20); // Adjust the number of stars as needed
      for (let i = 0; i < count; i++) {
        stars.value.push(calculateStarPosition());
      }
    }

    // Update star positions
    function updateStars(): void {
      const elapsedTime = (Date.now() - startTime) / 1000;
      const acceleration = Math.pow(elapsedTime, 2) * accelerationRate; // Exponential acceleration

      stars.value.forEach((star) => {
        star.y += star.speed * (1 + acceleration);

        // Reset star position if it moves off the screen
        if (star.y > window.innerHeight) {
          Object.assign(star, calculateStarPosition());
        }

        star.style = {
          left: `${star.x}px`,
          top: `${star.y}px`,
          opacity: `${star.opacity}`,
        };
      });
    }

    onMounted(() => {
      createStars();
      animateInterval.value = setInterval(updateStars, 1000 / 60); // Adjust the animation speed as needed
    });

    onUnmounted(() => {
      clearInterval(animateInterval.value);
    });

    return {
      stars: computed(() => stars.value),
    };
  },
});
</script>

<style scoped>
.space-background {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: #000000; /* Dark background color */
  overflow: hidden;
  z-index: -1;
}

.stars {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
}

.star {
  position: absolute;
  top: 0;
  left: 0;
  width: 2px;
  height: 2px;
  background-color: #ffffff;
  pointer-events: none;
}
</style>