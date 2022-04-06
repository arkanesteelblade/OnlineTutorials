#ifndef GAMELOOP_H
#define GAMELOOP_H

#include "SDL2/SDL.h"
#include <iostream>

class GameLoop
{
    public:
        GameLoop();
        virtual ~GameLoop();

        void Init(const char* title, int x_pos, int y_pos, int width, int height, bool full_screen);
        void HandleEvents();
        void Update();
        void Render();
        void CleanUp();

        bool Running() { return is_running; }
    private:
        int counter = 0;
        bool is_running = false;
        SDL_Window* window;
        SDL_Renderer* renderer;

};

#endif // GAMELOOP_H
