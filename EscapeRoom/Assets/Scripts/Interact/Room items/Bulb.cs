using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeRoom.Interact.Item
{
    public class Bulb : Pickup
    {
        [Header("Bulb light materials")]
        [SerializeField] Material yellowLight;
        [SerializeField] Material blueLight;
        [SerializeField] Material greenLight;
        [SerializeField] Material purpleLight;
        [SerializeField] Material pinkLight;
        [SerializeField] Material redLight;
        [SerializeField] Material orangeLight;
        [SerializeField] Material noLight;

        MeshRenderer meshRenderer;

        protected override void Awake()
        {
            base.Awake();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public void SetLight(Colour lightColour)
        {
            switch(lightColour)
            {
                case Colour.Blue:
                    meshRenderer.material = blueLight;
                    break;
                case Colour.Red:
                    meshRenderer.material = redLight;
                    break;
                case Colour.Green:
                    meshRenderer.material = greenLight;
                    break;
                case Colour.Pink:
                    meshRenderer.material = pinkLight;
                    break;
                case Colour.Purple:
                    meshRenderer.material = purpleLight;
                    break;
                case Colour.Yellow:
                    meshRenderer.material = yellowLight;
                    break;
                case Colour.Orange:
                    meshRenderer.material = orangeLight;
                    break;
                case Colour.None:
                    meshRenderer.material = noLight;
                    break;
            }
        }
    }
}

