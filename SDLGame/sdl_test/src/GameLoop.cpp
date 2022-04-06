#include "GameLoop.h"

GameLoop::GameLoop()
{
    //ctor
}

GameLoop::~GameLoop()
{
    //dtor
}
void GameLoop::Init(const char* title, int x_pos, int y_pos, int width, int height, bool full_screen)
{
    int flags = 0;

    is_running = false;

    if ( full_screen )
        flags = SDL_WINDOW_FULLSCREEN;

    if ( SDL_Init(SDL_INIT_EVERYTHING) == 0)
    {
        std::cout << "Subsystem Initialized"<<std::endl;
        window = SDL_CreateWindow(title,x_pos, y_pos,width,height,flags);

        if ( window )
        {
            std::cout << "Game windows created!"<<std::endl;
            renderer = SDL_CreateRenderer(window,-1,0);
            if ( renderer )
            {
                std::cout<<"Window Renderer created!"<<std::endl;
                SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);
                is_running = true;
            }
        }
    }
}
void GameLoop::HandleEvents()
{
    SDL_Event event;
    SDL_PollEvent(&event);

    switch (event.type)
    {
        case SDL_QUIT:
            is_running = false;
        break;
        default:
        break;
    }
}
void GameLoop::Update()
{
    counter++;
}
void GameLoop::Render()
{
    SDL_RenderClear(renderer);
    //TODO: render stuff
    SDL_RenderPresent(renderer);
}
void GameLoop::CleanUp()
{
    SDL_DestroyWindow(window);
    SDL_DestroyRenderer(renderer);
    SDL_Quit();
    std::cout<<"Game clenaed...done"<<std::endl;
}
