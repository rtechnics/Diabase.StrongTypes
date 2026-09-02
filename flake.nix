{
  description = "Diabase.StrongTypes";
  nixConfig = {
    extra-trusted-public-keys = [
      "devenv.cachix.org-1:w1cLUi8dv3hnoSPGAuibQv+f9TZLr6cv/Hm9XgU50cw="
      "nix-community.cachix.org-1:mB9FSh9qf2dCimDSUo8Zy7bkq5CX+/rkCWyvRCYg3Fs="
    ];
    extra-substituters = [
      "https://devenv.cachix.org"
      "https://nix-community.cachix.org"
    ];
  };
  inputs = {
    devenv = {
      url = "github:cachix/devenv";
      inputs = {
        flake-parts.follows = "flake-parts";
        git-hooks.follows = "git-hooks-nix";
        nixd.follows = "nixd";
        nixpkgs.follows = "nixpkgs-devenv";
      };
    };

    flake-parts = {
      url = "github:hercules-ci/flake-parts";
      inputs.nixpkgs-lib.follows = "nixpkgs";
    };
    git-hooks-nix = {
      url = "github:cachix/git-hooks.nix";
      inputs.nixpkgs.follows = "nixpkgs-devenv";
    };
    nixd = {
      url = "github:nix-community/nixd";
      inputs = {
        flake-parts.follows = "flake-parts";
        nixpkgs.follows = "nixpkgs-devenv";
      };
    };
    nixpkgs.url = "github:NixOS/nixpkgs/nixpkgs-unstable";
    nixpkgs-devenv.url = "github:cachix/devenv-nixpkgs/rolling";
    treefmt-nix = {
      url = "github:numtide/treefmt-nix";
      inputs.nixpkgs.follows = "nixpkgs-devenv";
    };
  };
  outputs =
    inputs@{ flake-parts, ... }:
    flake-parts.lib.mkFlake { inherit inputs; } {
      imports = [ inputs.devenv.flakeModule ];
      systems = inputs.nixpkgs.lib.systems.flakeExposed;
      perSystem = { config, pkgs, ... }: {
        devenv.shells.default = import ./devenv.nix;
        packages = {
          default = config.packages.diabase-strongtypes;
          # TODO: To finish packing with Nix, we would need to follow the instructions to package the Nuget dependencies:
          # https://nixos.org/manual/nixpkgs/unstable/#generating-and-updating-nuget-dependencies
          diabase-strongtypes = pkgs.buildDotnetModule {
            name = "Diabase.StrongTypes";
            src = ./.;
            packNupkg = true;
            dotnet-sdk =
              pkgs.dotnetCorePackages."sdk_${config.devenv.shells.default.languages.dotnet.packageSuffix}";
            dotnet-runtime =
              pkgs.dotnetCorePackages."runtime_${config.devenv.shells.default.languages.dotnet.packageSuffix}";
          };
        };
      };
    };
}
