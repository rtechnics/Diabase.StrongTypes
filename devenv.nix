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
    packages = [
      pkgs.wrkflw
      pkgs.yaml-language-server
    ];
    languages.dotnet = {
      enable = true;
      package = pkgs.dotnetCorePackages."sdk_${config.languages.dotnet.packageSuffix}";
    };
    treefmt = {
      enable = true;
      config.programs = {
        nixfmt = {
          enable = true;
          strict = true;
        };
        pinact.enable = true;
        shfmt.enable = true;
        yamlfmt.enable = true;
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
