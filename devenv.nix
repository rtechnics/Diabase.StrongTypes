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
      # wrkflw also exists as an actions runner, but it has the following issues:
      # - https://github.com/bahdotsh/wrkflw/pull/123 - Needs GITHUB_ACTION_PATH set
      # - https://github.com/bahdotsh/wrkflw/issues/113 - Uses hard-coded images that lose compatibility with GitHub Actions
      pkgs.act
      pkgs.yaml-language-server
    ];
    languages = {
      dotnet = {
        enable = true;
        package = pkgs.dotnetCorePackages."sdk_${config.languages.dotnet.packageSuffix}";
      };
      nix.enable = true;
      shell.enable = true;
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
