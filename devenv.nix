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
        shfmt.enable = true;
      };
    };
    git-hooks.hooks = {
      flake-checker.enable = true;
      shellcheck.enable = true;
    };
  };
}
