using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Animal
{
    public class AnimalType
    {
        /// <summary>
        /// elephant
        /// </summary>
        public static int elephant = 8;
        /// <summary>
        /// lion
        /// </summary>
        public static int lion = 7;
        /// <summary>
        /// tiger
        /// </summary>
        public static int tiger = 6;
        /// <summary>
        /// panther
        /// </summary>
        public static int panther = 5;
        /// <summary>
        /// wolf
        /// </summary>
        public static int wolf = 4;
        /// <summary>
        /// dog
        /// </summary>
        public static int dog = 3;
        /// <summary>
        /// cat
        /// </summary>
        public static int cat = 2;
        /// <summary>
        /// mouse
        /// </summary>
        public static int mouse = 1;

        /// <summary>
        /// determine which animal beats which
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns></returns>
        public static bool canEat(int a1, int a2)
        {
            //if mouse and elephant, mouse beats elephant
            if (a1 == 1 && a2 == 8)
                return true;
            //elephant cannot beat mouse
            if (a1 == 8 && a2 == 1)
                return false;
            //if all others
            if (a1 >= a2)
                return true;
            //return no one beats no one
            return false;
        }
        /// <summary>
        /// return string of animal names
        /// </summary>
        /// <param name="animalType"></param>
        /// <returns></returns>
        /* Version 1
         * public static string getText(int animalType)
        {
            switch (animalType)
            {
                case 1:
                    return "mouse";
                case 2:
                    return "cat";
                case 3:
                    return "dog";
                case 4:
                    return "wolf";
                case 5:
                    return "panther";
                case 6:
                    return "tiger";
                case 7:
                    return "lion";
                case 8:
                    return "elephant";
            }
            return null;
        }
        */
        /// Version 2 <summary>
        /// return RED pictures according to types of animals
        /// </summary>
        /// <param name="animalType"></param>
        /// <returns></returns>
        public static string getImageRed(int animalType)
        {
           
            switch (animalType)
            {
                case 1:
                    return "imgs/老鼠红.png";
                case 2:
                    return "imgs/猫红.png";
                case 3:
                    return "imgs/狗红.png";
                case 4:
                    return "imgs/狼红.png";
                case 5:
                    return "imgs/豹子红.png";
                case 6:
                    return "imgs/老虎红.png";
                case 7:
                    return "imgs/狮子红.png";
                case 8:
                    return "imgs/大象红.png";
            }
            return null;
        }


        /// <summary>
        /// return BLUE pictures according to types of animals
        /// </summary>
        /// <param name="animalType"></param>
        /// <returns></returns>
        public static string getImageBlue(int animalType)
        {
            switch (animalType)
            {
                case 1:
                    return "imgs/老鼠蓝.png";
                case 2:
                    return "imgs/猫蓝.png";
                case 3:
                    return "imgs/狗蓝.png";
                case 4:
                    return "imgs/狼蓝.png";
                case 5:
                    return "imgs/豹子蓝.png";
                case 6:
                    return "imgs/老虎蓝.png";
                case 7:
                    return "imgs/狮子蓝.png";
                case 8:
                    return "imgs/大象蓝.png";
            }
            return null;
        }



    }
}
