<template>
    <div class="asteroid-container">
      <img v-for="(asteroid, index) in asteroids" :key="index" class="asteroid" :src="asteroid.src" :style="asteroid.style" />
    </div>
  </template>
  
  <script lang="ts">
  import { defineComponent, ref, onMounted, onUnmounted } from 'vue';
  import asteroidImage from '@/assets/asteroid.png';
  
  interface Asteroid {
    src: string;
    size: string;
    x: number;
    y: number;
    speed: number;
    style: { [key: string]: string };
  }
  
  export default defineComponent({
    name: 'AsteroidField',
    props: {
      spaceshipZIndex: {
        type: Number,
        required: true,
      },
    },
    setup(props) {
      const asteroids = ref<Asteroid[]>([]);
      const asteroidInterval = ref<number | undefined>(undefined);
      const screenWidth = window.innerWidth;
      const screenHeight = window.innerHeight;
      const accelerationRate = 0.001; // Adjust the acceleration rate as needed
  
      function createAsteroid(): Asteroid {
        const minSize = props.spaceshipZIndex.toString();
        const maxSize = `${Math.floor(screenWidth * 0.25)}px`;
        const size = `${Math.floor(Math.random() * (parseInt(maxSize) - parseInt(minSize) + 1)) + parseInt(minSize)}px`;
        const x = Math.random() * (screenWidth - parseInt(size));
        const y = -parseInt(size);
        const speed = Math.random() + 1; // Adjust the initial speed as needed
  
        return {
          src: asteroidImage,
          size,
          x,
          y,
          speed,
          style: {
            width: size,
            height: size,
            position: 'absolute',
            top: `${y}px`,
            left: `${x}px`,
            zIndex: props.spaceshipZIndex.toString(),
          },
        };
      }
  
      function updateAsteroids(): void {
        const elapsedTime = (Date.now() - startTime) / 1000;
        const acceleration = Math.pow(elapsedTime, 2) * accelerationRate * 0.5; // Adjust the acceleration factor
  
        asteroids.value.forEach((asteroid) => {
          asteroid.y += asteroid.speed * (1 + acceleration); // Adjust the speed formula as needed
  
          // Remove asteroid when it goes off the screen
          if (asteroid.y > screenHeight) {
            asteroids.value.splice(asteroids.value.indexOf(asteroid), 1);
          }
  
          asteroid.style.top = `${asteroid.y}px`;
        });
      }
  
      let startTime = 0;
  
      onMounted(() => {
        startTime = Date.now(); // Store the start time when the component is mounted
  
        // Generate asteroids at regular intervals
        asteroidInterval.value = setInterval(() => {
          asteroids.value.push(createAsteroid());
        }, 1000); // Adjust the interval as needed
      });
  
      onUnmounted(() => {
        clearInterval(asteroidInterval.value);
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
    z-index: 1; /* Adjust the z-index to ensure it's above the SpaceBackground */
  }
  
  .asteroid {
    object-fit: contain;
  }
  </style>