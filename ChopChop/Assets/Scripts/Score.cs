﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace scoring
{

    public class Score : MonoBehaviour
    {

        public Text scoreText;
        public Text goldText;
        public Text enemyKilledText;
        private static int totalPoints;
        private static int totalGold;
        private static int waveNum;
        private static int enemyKiled;

        // Start is called before the first frame update
        void Start()
        {
            totalPoints = 0;
            totalGold = 0;
            enemyKiled = 0;
            waveNum = 1;
        }

        // Update is called once per frame
        void Update()
        {
            scoreText.text = "<color=#0D3CD7>" + totalPoints.ToString() + "</color>";
            goldText.text = "<color=#D79F0D>" + totalGold.ToString() + "</color>";
            enemyKilledText.text = "<color=#1B1B1B>" + enemyKiled.ToString() + "</color>";
        }

        public static void addScore(int gold, int points)
        {
            totalPoints += points * waveNum;
            totalGold += gold * waveNum;
        }

        public static void useGold(int gold)
        {
            totalGold -= gold;
        }

        public static int getTotalScore()
        {
            return totalPoints;
        }

        public static int getGold()
        {
            return totalGold;
        }

        public static void setGold(int gold){
            totalGold = gold;
        }

        public static void setWave(int wave)
        {
            waveNum = wave;
        }

        public static int getWave()
        {
            return waveNum;
        }

        public static void addEnemyKilled()
        {
            enemyKiled += 1;
        }

        public static int getEnemyKilled()
        {
            return enemyKiled;
        }
    }
}