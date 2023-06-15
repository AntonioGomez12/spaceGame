<template>
  <div class="asteroid-container">
    <img
      v-for="(asteroid, index) in asteroids"
      :key="index"
      class="asteroid"
      :src="asteroid.src"
      :style="asteroid.style"
    />
  </div>
</template>

<script lang="ts">
import asteroidImage from '@/assets/asteroid.png'
import { defineComponent, ref, onMounted, onUnmounted } from 'vue'

interface Asteroid {
  src: string
  size: string
  x: number
  y: number
  speedY: number
  accelerationY: number
  rotationSpeed: number
  style: { [key: string]: string }
}

export default defineComponent({
  name: 'AsteroidField',
  setup() {
    const asteroids = ref<Asteroid[]>([])
    const asteroidInterval = ref<number>(0)
    const screenWidth = window.innerWidth
    const screenHeight = window.innerHeight
    //Can have asteroid rotation to make the game harder...
    const maxRotationSpeed = 1
     //Create rate factor for acceleration
    const accelerationFactor = 0.0001
    //Maximum speed for the asteroids coming downward.
    const maxSpeed = 10 
    // Amount of seconds before asteroids reach max speed (currently set for presentation)
    const gameplayDuration = 180 
    let startTime = 0

    function createAsteroid(): Asteroid {
      //minimum size of asteroid to be spawned
      const minSize = 40
      //max size spawned
      const maxSize = 100
      //creating randomly sized asteroids from min/max
      const size = Math.floor(Math.random() * (maxSize - minSize + 1)) + minSize
      //Not a good way to do this since its dependent on screen width..
      const x = Math.random() * screenWidth - 150
      const y = -size
       //Set initial asteroid speed
      const speedY = 0
      //Set initial asteroid acceleration
      const accelerationY = 0 
      //could use this to make game harder later on..
      const rotationSpeed = (Math.random() - 0.5) * 2 * maxRotationSpeed

      return {
        src: asteroidImage,
        size: `${size}px`,
        x,
        y,
        speedY,
        accelerationY,
        rotationSpeed,
        style: {
          width: `${size}px`,
          height: `${size}px`,
          position: 'absolute',
          top: `${y}px`,
          left: `${x}px`,
          zIndex: '1'
        }
      }
    }

    //function to update asteroids and make more spawn as game play time increases
    function updateAsteroids(): void {
      
      //use time factors
      const currentTime = Date.now()
      const deltaTime = (currentTime - startTime) / 1000
      startTime = currentTime

      asteroids.value.forEach((asteroid) => {
        // Acceleration based on the exponential growth formula
        const acceleration = accelerationFactor * deltaTime

        // Apply the acceleration to the speed
        asteroid.accelerationY += acceleration

        // Limit the acceleration to the maximum value
        asteroid.accelerationY = Math.min(asteroid.accelerationY, maxSpeed)

        // Apply the acceleration to the speed
        asteroid.speedY += asteroid.accelerationY

        // Limit the speed to the maximum value
        asteroid.speedY = Math.min(asteroid.speedY, maxSpeed)

        // Update the position using the speed
        asteroid.y += asteroid.speedY

        // Can use this later on to increase difficulty..
        asteroid.style.transform = `translate(${asteroid.x}px, ${asteroid.y}px) rotate(${asteroid.rotationSpeed}deg)`

        if (asteroid.y > screenHeight + parseInt(asteroid.size)) {
          asteroid.y = -parseInt(asteroid.size)
          asteroid.x = Math.random() * screenWidth
        }
      })

      requestAnimationFrame(updateAsteroids)
    }

    //function to create spurts of asteroids to increase difficulty..
    function startAsteroidSpurt(): void {
      // Randomly spawn asteroids
      const asteroidCount = Math.floor(Math.random() * 2) 

      //Exponential growth formula will cause more asteroids to spurt as game progresses
      for (let i = 0; i < asteroidCount; i++) {
        asteroids.value.push(createAsteroid())
      }
    }

    onMounted(() => {
      startTime = Date.now()
      // Interval between asteroid spurts for finer tuning
      asteroidInterval.value = setInterval(startAsteroidSpurt, 3000) 
      requestAnimationFrame(updateAsteroids)
    })

    onUnmounted(() => {
      if (asteroidInterval.value) {
        clearInterval(asteroidInterval.value)
      }
    })

    return {
      asteroids
    }
  }
})
</script>

<style scoped>
.asteroid-container {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  pointer-events: none;
  z-index: 1;
}

.asteroid {
  object-fit: contain;
}
</style>
