public class AtaqueDuplo implements Movimento{
    Saint golpeador;
    Saint golpeado;
    
    public AtaqueDuplo(Saint golpeador, Saint golpeado) {
        this.golpeador = golpeador;
        this.golpeado = golpeado;
    }

    private double preGolpe() throws Exception {

        int dano = this.golpeador.getProximoGolpe().getFatorDano();

        if(golpeador.getArmaduraVestida()) {
            return dano *= this.golpeador.getNivelArmadura() +1;
        }else{
            return dano;
        }
    }

    public void executar() throws Exception {

        DadoD6 random = new DadoD6();

        double dano = this.preGolpe();
        if(random.chances30PorCento()){
            dano *= 2;
            this.golpeado.perderVida(dano);
        }else{
            this.golpeado.perderVida(dano);
			double perdaGolpeador = (this.golpeador.getVida() * 0.05);
			this.golpeador.perderVida(perdaGolpeador);
        }
    }
}