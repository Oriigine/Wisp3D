using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Les floats que le code utilise pour g�rer la vitesse de d�placement du joueur (affich�e en inspecteur pour des raisons de debug)
    [SerializeField]
    private float HorizontalSpeed = 0f;
    [SerializeField]
    private float VerticalSpeed = 0f;

    //La vitesse maximale que le joueur peut atteindre (changeable en inspecteur)
    [SerializeField]
    private float MaxSpeed = 0f;

    //Le facteur d'acc�l�ration du joueur (changeable en inspecteur)
    [SerializeField]
    private float Acceleration = 0f;

    //Le facteur de d�c�l�ration du joueur (changeable en inspecteur)
    [SerializeField]
    private float Deceleration = 0f;

    //Le bool�en indiquant si le d�placement est normalized ou non (oui j'ai fait un normalize � la main parce que le normalize d'unity ne marchait pas, ne me demandez pas pourquoi)
    private bool normalized = false;

    //Les bool�ens indiquant si on a appuy� sur les inputs horizontaux
    private bool D_Pressed;
    private bool Q_Pressed;

    //Pareil mais pour les inputs verticaux
    private bool Z_Pressed;
    private bool S_Pressed;


    //IMPORTANT
    //IMPORTANT
    //J'ai s�par� dans le code le fontionnement des axes horizontaux et verticaux pour plus de clart�, donc quand j'explique le code, j'explique d'abord pour les axes horizontaux et j'indique dans un second temps que c'est la m�me chose mais pour les axes verticaux
    //IMPORTANT
    //IMPORTANT


    void Update()
    {
        #region DETECTION DES TOUCHES
        //Ici, on d�tecte simplement les imputs du joueur, et on les traduit par des bool�ens utilisables plus tard

        //Si on appuie sur D, on active un bool�en
        if (Input.GetKey(KeyCode.D))
        {
            D_Pressed = true;
        }

        //Si on appuie sur Q, on active un bool�en
        if (Input.GetKey(KeyCode.Q))
        {
            Q_Pressed = true;
        }

        //Si on rel�che D, on d�sactive un bool�en
        if (Input.GetKeyUp(KeyCode.D))
        {
            D_Pressed = false;
        }

        //Si on rel�che Q, on d�sactive un bool�en
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Q_Pressed = false;
        }



        //Si on appuie sur Z, on active un bool�en
        if (Input.GetKey(KeyCode.Z))
        {
            Z_Pressed = true;
        }

        //Si on appuie sur S, on active un bool�en
        if (Input.GetKey(KeyCode.S))
        {
            S_Pressed = true;
        }

        //Si on rel�che Z, on d�sactive un bool�en
        if (Input.GetKeyUp(KeyCode.Z))
        {
            Z_Pressed = false;
        }

        //Si on rel�che S, on d�sactive un bool�en
        if (Input.GetKeyUp(KeyCode.S))
        {
            S_Pressed = false;
        }
        #endregion DETECTION DES TOUCHES

        #region TOUCHES PRESSEES
        //Ici, on g�re ce que le code va faire quand on appuie sur des touches ; de fa�on g�n�rale, on a ici les d�placements et le drag


        //Si la touche D est maintenue, on se d�place.
        if (D_Pressed == true)
        {
            //Si on n'a pas atteint la vitesse maximale, on acc�l�re pendant notre d�placement
            if(HorizontalSpeed < MaxSpeed)
            {
                transform.position += new Vector3(HorizontalSpeed, 0, 0);
                HorizontalSpeed += Acceleration * Time.deltaTime;
            }
            //Quand on a atteint la vitesse maximale, on se contente de se d�placer
            else
            {
                transform.position += new Vector3(HorizontalSpeed, 0, 0);
            }
        }

        //Pareil qu'au-dessus, mais on inverse les signes des nombres qu'on utilise pour se d�placer dans l'autre sens
        if (Q_Pressed == true)
        {
            //Ici, on soustrait le facteur d'acc�l�ration au lieu de l'additionner pour aller dans l'autre sens
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


        //Pareil qu'au-dessus, mais pour l'axe vertical
        
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



        //Ici, on normalise les vecteurs de d�placements pour ne pas aller plus vite en diagonale ; la m�thode normalize ne marchait pas, et �a allait plus vite de faire �a plut�t que de faire fonctionner la m�thode, donc je l'ai fait � la main
        //On va v�rifier qu'au moins une diagonale est utilis�e ET que la MaxSpeed n'est pas normalis�e, puis on divise MaxSpeed par racine de 2 et on indique au code que la vitesse est bien normalis�e pour ne pas qu'il passe plusieurs fois dans la boucle
        
        //Diagonale Z+D
        if (Z_Pressed && D_Pressed && normalized == false)
        {
            MaxSpeed = MaxSpeed / 1.41f;
            normalized = true;
        }
        //Diagonale Z+Q
        else if (Z_Pressed && Q_Pressed && normalized == false)
        {
            MaxSpeed = MaxSpeed / 1.41f;
            normalized = true;
        }
        //Diagonale S+D
        else if (S_Pressed && D_Pressed && normalized == false)
        {
            MaxSpeed = MaxSpeed / 1.41f;
            normalized = true;
        }
        //Diagonale S+Q
        else if (S_Pressed && Q_Pressed && normalized == false)
        {
            MaxSpeed = MaxSpeed / 1.41f;
            normalized = true;
        }

        //Le script ne passera ici que si aucune diagonale n'est utilis�e (puisqu'il les v�rifie toutes une par une avant d'arriver ici), et si la vitesse est normalis�e alors on la r�tablit � sa valeur d'origine
        else if (normalized == true)
        {
            MaxSpeed = MaxSpeed * 1.41f;
            normalized = false;
        }



        //Ici, on rev�rifie par s�curit� si les vitesses ne sont pas au-dessus de la MaxSpeed pour les r�ajuster, c.f lignes 101 et 117
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

        #endregion TOUCHES PRESSEES

        #region TOUCHES RELACHEES
        //Ici, on g�re ce que le code va faire lorsqu'on n'appuie pas sur les touches ; de fa�on g�n�rale, on a ici les drags de ralentissements et les bugfix de tremblements


        //Si aucune touche n'est press�e ET qu'on bouge vers la droite, alors on ralentit
        if (D_Pressed == false && Q_Pressed == false && HorizontalSpeed > 0)
        {
            transform.position += new Vector3(HorizontalSpeed, 0, 0);
            HorizontalSpeed -= Deceleration * Time.deltaTime;
        }

        //Si aucune touche n'est press�e ET qu'on bouge vers la gauche, alors on ralentit
        if (D_Pressed == false && Q_Pressed == false && HorizontalSpeed < 0)
        {
            transform.position += new Vector3(HorizontalSpeed, 0, 0);
            HorizontalSpeed += Deceleration * Time.deltaTime;
        }

        //Si aucune touche n'est press�e ET qu'on est tr�s tr�s lent, alors on s'arr�te (bugfix tremblement)
        if (D_Pressed == false && Q_Pressed == false && HorizontalSpeed >= -0.01 && HorizontalSpeed <= 0.01)
        {
            HorizontalSpeed = 0;
        }


        //Pareil mais pour l'axe vertical
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

        #endregion TOUCHES RELACHEES
    }
}
