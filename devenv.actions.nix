{ config, pkgs, ... }: {
  name = "diabase-strongtypes-actions";
  packages = [ pkgs.act ];
  containers.shell = {
    name = "${config.name}-shell";
    copyToRoot = pkgs.linkFarm "actions-env" [
      {
        name = ".config/act/actrc";
        path = ./.actrc.container-linux;
      }
    ];
    layers = [ { copyToRoot = [ pkgs.dockerTools.caCertificates ]; } ];
  };
}
