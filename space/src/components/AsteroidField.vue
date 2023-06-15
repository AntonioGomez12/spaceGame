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
import asteroidImage from "@/assets/asteroid.png";
import { defineComponent, ref, onMounted, onUnmounted } from "vue";

interface Asteroid {
  src: string;
  size: string;
  x: number;
  y: number;
  speedY: number;
  accelerationY: number;
  rotationSpeed: number;
  style: { [key: string]: string };
}

export default defineComponent({
  name: "AsteroidField",
  setup() {
    const asteroids = ref<Asteroid[]>([]);
    const asteroidInterval = ref<number | undefined>(undefined);
    const screenWidth = window.innerWidth;
    const screenHeight = window.innerHeight;
    const maxRotationSpeed = 1;
    const accelerationFactor = 0.0001; // Acceleration rate factor
    const maxSpeed = 10; // Maximum speed for the asteroids coming downward.
    const gameplayDuration = 180; // Amount of seconds before asteroids reach max speed (currently set for presentation)
    let startTime = 0;

    function createAsteroid(): Asteroid {
       //minimum size of asteroid to be spawned
      const minSize = 40; 
       //max size spawned 
      const maxSize = 100; 
      //creating randomly sized asteroids from min/max
      const size = Math.floor(Math.random() * (maxSize - minSize + 1)) + minSize; 
      const x = Math.random() * screenWidth -150;
      const y = -size;
      const speedY = 0; // Start with zero initial speed
      const accelerationY = 0; // Start with zero initial acceleration
      const rotationSpeed = (Math.random() - 0.5) * 2 * maxRotationSpeed;

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
          position: "absolute",
          top: `${y}px`,
          left: `${x}px`,
          zIndex: "1",
        },
      };
    }

    function updateAsteroids(): void {
      const currentTime = Date.now();
      const deltaTime = (currentTime - startTime) / 1000;
      startTime = currentTime;

      asteroids.value.forEach((asteroid) => {
        // Calculate the acceleration based on the exponential growth formula
        const acceleration = accelerationFactor * deltaTime;

        // Apply the acceleration to the speed
        asteroid.accelerationY += acceleration;

        // Limit the acceleration to the maximum value
        asteroid.accelerationY = Math.min(asteroid.accelerationY, maxSpeed);

        // Apply the acceleration to the speed
        asteroid.speedY += asteroid.accelerationY;

        // Limit the speed to the maximum value
        asteroid.speedY = Math.min(asteroid.speedY, maxSpeed);

        // Update the position using the speed
        asteroid.y += asteroid.speedY;

        asteroid.style.transform = `translate(${asteroid.x}px, ${asteroid.y}px) rotate(${asteroid.rotationSpeed}deg)`;

        if (asteroid.y > screenHeight + parseInt(asteroid.size)) {
          asteroid.y = -parseInt(asteroid.size);
          asteroid.x = Math.random() * screenWidth;
        }
      });

      requestAnimationFrame(updateAsteroids);
    }

    function startAsteroidSpurt(): void {
      const asteroidCount = Math.floor(Math.random() * 2); // Randomly spawn between 1 and 2 asteroids in each spurt

      for (let i = 0; i < asteroidCount; i++) {
        asteroids.value.push(createAsteroid());
      }
    }

    onMounted(() => {
      startTime = Date.now();
      asteroidInterval.value = setInterval(startAsteroidSpurt, 3000); // Interval between asteroid spurts

      requestAnimationFrame(updateAsteroids);
    });

    onUnmounted(() => {
      if (asteroidInterval.value) {
        clearInterval(asteroidInterval.value);
      }
    });

    return {
      asteroids,
    };
  },
});
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
