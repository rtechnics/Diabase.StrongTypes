{
  config,
  lib,
  pkgs,
  ...
}:
{
  options = {
    languages.dotnet.packageSuffix = lib.mkOption {
      type = lib.types.str;
      default = "9_0";
    };
  };
  config = {
    name = "diabase-strongtypes";
    languages = {
      dotnet = {
        enable = true;
        package = pkgs.dotnetCorePackages."sdk_${config.languages.dotnet.packageSuffix}";
      };
      nix.enable = true;
      shell.enable = true;
    };
    scripts = {
      actions-build.exec = "${lib.getExe pkgs.nix} run .#actions-container-shell.copyToPodman --impure --accept-flake-config";
      actions-bash.exec = "${lib.getExe pkgs.podman-compose} -f compose.actions_linux.yaml run --rm shell -- bash $@";
      act.exec = "${lib.getExe pkgs.podman-compose} -f compose.actions_linux.yaml run --rm shell -- act $@";
    };
    packages = [
      pkgs.docker-compose-language-service
      pkgs.podman-compose
      pkgs.yaml-language-server
    ];
    treefmt = {
      enable = true;
      config.programs = {
        nixfmt = {
          enable = true;
          strict = true;
        };
        pinact.enable = true;
        shfmt.enable = true;
        yamlfmt = {
          enable = true;
          settings = {
            formatter = {
              type = "basic";
              include_document_start = true; # yamllint fails without this
            };
          };
        };
      };
    };
    git-hooks.hooks = {
      actionlint.enable = true;
      flake-checker.enable = true;
      shellcheck.enable = true;
      yamllint.enable = true;
    };
  };
}
