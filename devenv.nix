{ pkgs, ... }: {
  languages.dotnet = {
    enable = true;
    package = with pkgs.dotnetCorePackages; combinePackages [ sdk_9_0 ];
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
}
