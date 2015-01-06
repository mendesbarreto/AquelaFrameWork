AquelaFrameWork
===============

Aquela Frame Work

Release Notes - Aquela FrameWork - Version 1.1.4

Bug
---
* [AF-12] - Problema na Assetmanager: DPI Folder não está sendo selecionada corretamente
* [AF-48] - AFDestroy() dos states não estãvão sendo chamados
* [AF-49] - AFAssetManager.Add( GameObject ) problema para carregar prefabs
* [AF-53] - Problema no reaproveitamento de Assets do AssetManager, nem todos estão sendo pegos corretamente
* [AF-57] - Unity3D sobrescreve o operador " == ou != ", de forma que não sabemos se eles estão nulos ou não
* [AF-60] - Problema quando o AState ia destruir os GameObjects.
* [AF-63] - Operação invalida na ora de criar o AFSound, estava sendo utilizado o operador "new", agora é necessario utlizar AFSound.Create()

Improvement
---
* [AF-50] - Suporte a simulação de Resolução e plataform no AFAssetManger 
* [AF-50] - Suporte a indicação de qual pasta é a raiz do projeto, ou seja, fazer subpastas virarem a pasta principal de um determinado projeto.
* [AF-44] - Suporte NGUI:UISprite2D nas animações da AFMovieclip
* [AF-46] - Suporte a simulador de DPI
* [AF-54] - Suporte ao Instantiate() no AFTextureAtlas para criar Sprites mais rápidos.
* [AF-55] - Inserido propriedade name no AFMovieClip
* [AF-56] - Função de verificação se objeto da engine é nulo ou não.
* [AF-58] - Inclusão da Propriedade do tipo Sprite no AFTextureInfo
* [AF-61] - Suporte ao Instantiate no AFAssetManager de objetos já carregados.
* [AF-62] - AFSoundManager agora com suporte a AFAssetManger, assets compartilhado salvado memoria alocada.
* [AF-64] - Novas formas de adicionar sons na sound manager
* [AF-47] - Suporte Responsive-Mult-resolution. O Componente fará o "SpriteRenderer" buscar resolução correta, mediante ao dispositivo usado.
