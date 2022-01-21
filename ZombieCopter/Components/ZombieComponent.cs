// -----------------------------------------------------------------------
// <copyright file="ZombieComponent.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ZombieCopter.Components
{
    using System;
    using System.Collections.Generic;
    using Exiled.API.Features;
    using MEC;
    using UnityEngine;

    /// <summary>
    /// Handles the zombie's ability to helicopter.
    /// </summary>
    public class ZombieComponent : MonoBehaviour
    {
        private Config config;
        private Player player;
        private Quaternion lastRotation;
        private int boostAmount;

        /// <summary>
        /// Gets or sets a value indicating whether a cooldown should be applied to a user's flying.
        /// </summary>
        public bool HasCooldown { get; set; } = true;

        private void Awake()
        {
            config = Plugin.Instance.Config;
            player = Player.Get(gameObject);
            player.ShowHint(config.Hint, config.HintDuration);
        }

        private void Update()
        {
            Quaternion rotation = transform.rotation;
            Vector3 angularVelocity = GetAngularVelocity(lastRotation, rotation);
            lastRotation = rotation;

            if (angularVelocity.magnitude > config.MinimumRotation && config.MaximumBoosts > boostAmount)
            {
                if (HasCooldown && boostAmount <= 0)
                    Timing.RunCoroutine(RunCooldown());

                for (int i = 0; i < config.BoostedUnits; i++)
                {
                    player.Position += Vector3.up;

                    if (config.PlayFlyNoise)
                        player.ReferenceHub.falldamage.RpcDoSound();
                }

                boostAmount++;
            }
        }

        private Vector3 GetAngularVelocity(Quaternion foreLastFrameRotation, Quaternion lastFrameRotation)
        {
            Quaternion q = lastFrameRotation * Quaternion.Inverse(foreLastFrameRotation);
            if (Math.Abs(q.w) > 1023.5f / 1024.0f)
                return Vector3.zero;

            float gain;
            if (q.w < 0.0f)
            {
                double angle = Math.Acos(-q.w);
                gain = (float)(-2.0f * angle / (Math.Sin(angle) * Time.deltaTime));
            }
            else
            {
                double angle = Math.Acos(q.w);
                gain = (float)(2.0f * angle / (Math.Sin(angle) * Time.deltaTime));
            }

            return new Vector3(q.x * gain, q.y * gain, q.z * gain);
        }

        private IEnumerator<float> RunCooldown()
        {
            yield return Timing.WaitForSeconds(config.SecondsPerInterval);
            boostAmount = 0;
        }
    }
}