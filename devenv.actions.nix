{ config, pkgs, ... }: {
  name = "diabase-strongtypes-actions";
  containers.shell = {
    name = "${config.name}-shell";
    copyToRoot = pkgs.linkFarm "actions-env" [
      {
        name = ".config/act/actrc";
        path = ./.actrc.container-linux;
      }
    ];
    layers = [
      { copyToRoot = [ pkgs.dockerTools.caCertificates ]; }
      { copyToRoot = [ pkgs.act ]; }
    ];
  };
}
