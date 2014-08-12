using System;
using System.Collections.Generic;

using Signals;
using AFFrameWork.Input;
using AFFrameWork.Sound;

/****************************************************************************|
/****************************************************************************|
		        *                                                           *
		        *  .=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-.       *
		        *   |                     ______                     |      *
		        *   |                  .-"      "-.                  |      *
		        *   |                 /            \                 |      *
		        *   |     _          |              |          _     |      *
		        *   |    ( \         |,  .-.  .-.  ,|         / )    |      *
		        *   |     > "=._     | )(__/  \__)( |     _.=" <     |      *
		        *   |    (_/"=._"=._ |/     /\     \| _.="_.="\_)    |      *
		        *   |           "=._"(_     ^^     _)"_.="           |      *
		        *   |               "=\__|IIIIII|__/="               |      *
		        *   |              _.="| \IIIIII/ |"=._              |      *
		        *   |    _     _.="_.="\          /"=._"=._     _    |      *
		        *   |   ( \_.="_.="     `--------`     "=._"=._/ )   |      *
		        *   |    > _.="                            "=._ <    |      *
		        *   |   (_/                                    \_)   |      *
		        *   |                                                |      *
		        *   '-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-='      *
		        *                      Douglas warning you:                 *
		        *    Do not touch! Unless you know what you're doing        *
		        *************************************************************/


namespace AFFrameWork.Core
{
    class AFEngine : ASingleton<AFEngine>
    {
        public static readonly string VERSION = "0.0.1";
        public static readonly string FRAME_RATE = "60";

        protected bool m_running = false;
        
        protected double m_startTime = 0;
        protected double m_time = 0;
        protected double m_deltaTime = 0;

        public Signal<bool> OnPause = new Signal<bool>();
        public Signal<bool> OnEngineReady = new Signal<bool>();


        //private MDSInput m_input;
        //private MSSoundManager m_soundManager;

    }
}
