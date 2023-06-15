<template>
  <div class="space-background">
    <div class="stars">
      <div v-for="(star, index) in stars" :key="index" class="star" :style="star.style"></div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, computed, onMounted, onUnmounted } from 'vue'

interface Star {
  x: number
  y: number
  speed: number
  opacity: number
  style: { [key: string]: string }
}

export default defineComponent({
  name: 'SpaceBackground',
  setup() {
    const stars = ref<Star[]>([])
    const animateInterval = ref<Timer | null>(null)
    const startTime = Date.now()
    // Create max speed of stars factor
    const maxSpeed = 5 
    // Create rate of accel factor for tuning
    const accelerationRate = 0.001 

    // Calculate the position, speed, and add some opacity to stars
    function calculateStarPosition(): Star {
      const x = Math.random() * window.innerWidth
      const y = Math.random() * window.innerHeight
      //Initial star speed to simulate spaceship moving
      const speed = Math.random() + 1 
      //Star Opacity so they look more real
      const opacity = Math.random() * 0.5 + 0.5
      return { x, y, speed, opacity, style: {} }
    }

    // Create initial stars
    function createStars(): void {
      // Create number of stars to be floored
      const count = Math.floor(window.innerWidth / 20) 
      for (let i = 0; i < count; i++) {
        stars.value.push(calculateStarPosition())
      }
    }

    // Update star positions
    function updateStars(): void {
      const elapsedTime = (Date.now() - startTime) / 1000
      // Exponential acceleration
      const acceleration = Math.pow(elapsedTime, 2) * accelerationRate 

      stars.value.forEach((star) => {
        star.y += star.speed * (1 + acceleration)

        // Reset star position if it moves off the screen
        if (star.y > window.innerHeight) {
          Object.assign(star, calculateStarPosition())
        }

        star.style = {
          left: `${star.x}px`,
          top: `${star.y}px`,
          opacity: `${star.opacity}`
        }
      })
    }

    onMounted(() => {
      createStars()
      // Star animation speeds
      animateInterval.value = setInterval(updateStars, 1000 / 60) 
    })

    onUnmounted(() => {
      clearInterval(animateInterval.value)
    })

    return {
      stars: computed(() => stars.value)
    }
  }
})
</script>

<style scoped>
.space-background {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: #000000;
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
