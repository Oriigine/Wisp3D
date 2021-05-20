using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Les floats que le code utilise pour gérer la vitesse de déplacement du joueur (affichée en inspecteur pour des raisons de debug)
    [SerializeField]
    private float HorizontalSpeed = 0f;
    [SerializeField]
    private float VerticalSpeed = 0f;

    //La vitesse maximale que le joueur peut atteindre (changeable en inspecteur)
    [SerializeField]
    private float MaxSpeed = 0f;

    //Le facteur d'accélération du joueur (changeable en inspecteur)
    [SerializeField]
    private float Acceleration = 0f;

    //Le facteur de décélération du joueur (changeable en inspecteur)
    [SerializeField]
    private float Deceleration = 0f;

    //Le booléen indiquant si le déplacement est normalized ou non (oui j'ai fait un normalize à la main parce que le normalize d'unity ne marchait pas, ne me demandez pas pourquoi)
    private bool normalized = false;

    //Les booléens indiquant si on a appuyé sur les inputs horizontaux
    private bool D_Pressed;
    private bool Q_Pressed;

    //Pareil mais pour les inputs verticaux
    private bool Z_Pressed;
    private bool S_Pressed;


    void Update()
    {
        #region KEYS DETECTION
        //Ici, on détecte simplement les imputs du joueur, et on les traduit par des booléens utilisables plus tard

        //Si on appuie sur D, on active un booléen
        if (Input.GetKey(KeyCode.D))
        {
            D_Pressed = true;
        }

        //Si on appuie sur Q, on active un booléen
        if (Input.GetKey(KeyCode.Q))
        {
            Q_Pressed = true;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            D_Pressed = false;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            Q_Pressed = false;
        }



        if (Input.GetKey(KeyCode.Z))
        {
            Z_Pressed = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            S_Pressed = true;
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            Z_Pressed = false;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            S_Pressed = false;
        }
        #endregion KEYS DETECTION

        #region MOUVEMENTS

        if (D_Pressed == true)
        {
            if(HorizontalSpeed < MaxSpeed)
            {
                transform.position += new Vector3(HorizontalSpeed, 0, 0);
                HorizontalSpeed += Acceleration * Time.deltaTime;
            }
            else
            {
                transform.position += new Vector3(HorizontalSpeed, 0, 0);
            }
        }

        if (Q_Pressed == true)
        {
            if(HorizontalSpeed > (MaxSpeed * -1))
            {
                transform.position += new Vector3(HorizontalSpeed, 0, 0);
                HorizontalSpeed -= Acceleration * Time.deltaTime;
            }
            else
            {
                transform.position += new Vector3(HorizontalSpeed, 0, 0);
            }

        }



        if (Z_Pressed == true)
        {
            if (VerticalSpeed < MaxSpeed)
            {
                transform.position += new Vector3(0, VerticalSpeed, 0);
                VerticalSpeed += Acceleration * Time.deltaTime;
            }
            else
            {
                transform.position += new Vector3(0, VerticalSpeed, 0);
            }
        }

        if (S_Pressed == true)
        {
            if (VerticalSpeed > (MaxSpeed * -1))
            {
                transform.position += new Vector3(0, VerticalSpeed, 0);
                VerticalSpeed -= Acceleration * Time.deltaTime;
            }
            else
            {
                transform.position += new Vector3(0, VerticalSpeed, 0);
            }

        }


        if (Z_Pressed && D_Pressed && normalized == false)
        {
            MaxSpeed = MaxSpeed / 1.41f;
            normalized = true;
        }
        else if (Z_Pressed && Q_Pressed && normalized == false)
        {
            MaxSpeed = MaxSpeed / 1.41f;
            normalized = true;
        }
        else if (S_Pressed && D_Pressed && normalized == false)
        {
            MaxSpeed = MaxSpeed / 1.41f;
            normalized = true;
        }
        else if (S_Pressed && Q_Pressed && normalized == false)
        {
            MaxSpeed = MaxSpeed / 1.41f;
            normalized = true;
        }
        else if (normalized == true)
        {
            MaxSpeed = MaxSpeed * 1.41f;
            normalized = false;
        }


        if (HorizontalSpeed >= MaxSpeed)
        {
            HorizontalSpeed = MaxSpeed;
        }

        if (HorizontalSpeed <= MaxSpeed * -1)
        {
            HorizontalSpeed = MaxSpeed * -1;
        }

        if (VerticalSpeed >= MaxSpeed)
        {
            VerticalSpeed = MaxSpeed;
        }

        if (VerticalSpeed <= MaxSpeed * -1)
        {
            VerticalSpeed = MaxSpeed * -1;
        }

        #endregion MOUVEMENTS

        #region SI PAS DE MOUVEMENT

        //Si aucune touche n'est pressée ET qu'on bouge vers la droite, alors on ralentit
        if (D_Pressed == false && Q_Pressed == false && HorizontalSpeed > 0)
        {
            transform.position += new Vector3(HorizontalSpeed, 0, 0);
            HorizontalSpeed -= Deceleration * Time.deltaTime;
        }

        //Si aucune touche n'est pressée ET qu'on bouge vers la gauche, alors on ralentit
        if (D_Pressed == false && Q_Pressed == false && HorizontalSpeed < 0)
        {
            transform.position += new Vector3(HorizontalSpeed, 0, 0);
            HorizontalSpeed += Deceleration * Time.deltaTime;
        }

        //Si on est à l'arrêt
        if (D_Pressed == false && Q_Pressed == false && HorizontalSpeed >= -0.01 && HorizontalSpeed <= 0.01)
        {
            HorizontalSpeed = 0;
        }



        if (Z_Pressed == false && S_Pressed == false && VerticalSpeed > 0)
        {
            transform.position += new Vector3(0, VerticalSpeed, 0);
            VerticalSpeed -= Deceleration * Time.deltaTime;
        }

        if (Z_Pressed == false && S_Pressed == false && VerticalSpeed < 0)
        {
            transform.position += new Vector3(0, VerticalSpeed, 0);
            VerticalSpeed += Deceleration * Time.deltaTime;
        }

        if (Z_Pressed == false && S_Pressed == false && VerticalSpeed >= -0.01 && VerticalSpeed <= 0.01)
        {
            VerticalSpeed = 0;
        }

        #endregion SI PAS DE MOUVEMENT
    }
}
