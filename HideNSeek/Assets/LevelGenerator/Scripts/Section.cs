using System.Collections;
using System.Linq;
using LevelGenerator.Scripts.Helpers;
using Unity.VisualScripting;
using UnityEngine;

namespace LevelGenerator.Scripts
{
    public class Section : MonoBehaviour
    {
        /// <summary>
        /// Section tags
        /// </summary>
        public string[] Tags;

        /// <summary>
        /// Tags that this section can annex
        /// </summary>
        public string[] CreatesTags;

        /// <summary>
        /// Exits node in hierarchy
        /// </summary>
        public Exits Exits;

        /// <summary>
        /// Bounds node in hierarchy
        /// </summary>
        public Bounds Bounds;

        /// <summary>
        /// Chances of the section spawning a dead end
        /// </summary>
        public int DeadEndChance;

        protected LevelGenerator LevelGenerator;
        protected int order;

        public Renderer cubeRenderer;
        Material myMaterial;

        public void Initialize(LevelGenerator levelGenerator, int sourceOrder)
        {
            LevelGenerator = levelGenerator;
            transform.SetParent(LevelGenerator.Container);
            LevelGenerator.RegisterNewSection(this);
            order = sourceOrder + 1;

            //RandomPosUnity();
            RandomPosProcedural();

            // TODO make its color hue change based on its position, so you get a red, orange, yellow, green, etc. zone you can keep track of
            float myPosX = Mathf.Clamp01((transform.position.x + 50f) / 100f);
            float myPosY = Mathf.Clamp01((transform.position.y + 50f) / 100f);
            float myPosZ = Mathf.Clamp01((transform.position.z + 50f) / 100f);
            myMaterial = new Material(cubeRenderer.material);
            myMaterial.color = Color.HSVToRGB(myPosX, myPosY, myPosZ);
            cubeRenderer.material = myMaterial;
            //cubeRenderer.material.color = new Color(cubeRenderer.material.color.r, cubeRenderer.material.color.g, cubeRenderer.material.color.b, cubeRenderer.material.color.a-0.5f);

            GenerateAnnexes();
        }

        void RandomPosUnity() // uses Unity's built in Random class which has its own seed
        {
            // can use floats and ints for random range
            transform.position += Vector3.up * Random.Range(-1f, 1f);
            int chance = Random.Range(1, 6);
            switch (chance)
            {
                case 1:
                    {
                        transform.Rotate(Random.Range(-15, 16), 0, 0);
                    }
                    break;
                case 2:
                    {
                        transform.Rotate(0, Random.Range(-15, 16), 0);
                    }
                    break;
                case 3:
                    {
                        transform.Rotate(0, 0, Random.Range(-15, 16));
                    }
                    break;
            }
        }

        void RandomPosProcedural() // uses the level generator's custom RandomService class which has its own seed
        {
            // currently restricted to ints only in random range
            transform.position += Vector3.up * RandomService.GetRandom(-1, 1);
            int chance = RandomService.GetRandom(1, 5);
            switch (chance)
            {
                case 1:
                    {
                        transform.Rotate(RandomService.GetRandom(-15, 15), 0, 0);
                    }
                    break;
                case 2:
                    {
                        transform.Rotate(0, RandomService.GetRandom(-15, 15), 0);
                    }
                    break;
                case 3:
                    {
                        transform.Rotate(0, 0, RandomService.GetRandom(-15, 15));
                    }
                    break;
            }
        }

        protected void GenerateAnnexes()
        {
            if (CreatesTags.Any())
            {
                foreach (var e in Exits.ExitSpots)
                {
                    if (LevelGenerator.LevelSize > 0 && order < LevelGenerator.MaxAllowedOrder)
                        if (RandomService.RollD100(DeadEndChance))
                            PlaceDeadEnd(e);
                        else
                            GenerateSection(e);
                    else
                        PlaceDeadEnd(e);
                }
            }
        }

        protected void GenerateSection(Transform exit)
        {
            var candidate = IsAdvancedExit(exit)
                ? BuildSectionFromExit(exit.GetComponent<AdvancedExit>())
                : BuildSectionFromExit(exit);
                
            if (LevelGenerator.IsSectionValid(candidate.Bounds, Bounds))
            {
                candidate.Initialize(LevelGenerator, order);
            }
            else
            {
                Destroy(candidate.gameObject);
                PlaceDeadEnd(exit);
            }
        }

        protected void PlaceDeadEnd(Transform exit) => Instantiate(LevelGenerator.DeadEnds.PickOne(), exit).Initialize(LevelGenerator);

        protected bool IsAdvancedExit(Transform exit) => exit.GetComponent<AdvancedExit>() != null;

        protected Section BuildSectionFromExit(Transform exit) => Instantiate(LevelGenerator.PickSectionWithTag(CreatesTags), exit).GetComponent<Section>();

        protected Section BuildSectionFromExit(AdvancedExit exit) => Instantiate(LevelGenerator.PickSectionWithTag(exit.CreatesTags), exit.transform).GetComponent<Section>();
    }
}