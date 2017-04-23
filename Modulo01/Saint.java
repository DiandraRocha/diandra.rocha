import java.util.ArrayList;
import java.security.InvalidParameterException; 

public abstract class Saint {

    private String nome;
    private Armadura armadura;
    private boolean armaduraVestida;
    private Genero genero = Genero.NAO_INFORMADO;
    private Status status = Status.VIVO;
    private double vida = 100.0;
    protected int qtdSentidosDespertados;
    private int acumuladorProximo = 0;
    private ArrayList <Movimento> movimentos;

    public Saint() {

    }

    public Saint(String nome, Armadura armadura) throws Exception {
        this.nome = nome;
        this.armadura = armadura;
    }

    public String getNome() {
        return this.nome;
    }

    public void vestirArmadura() {
        this.armaduraVestida = true;
    }

    public boolean getArmaduraVestida() {
        return this.armaduraVestida;
    }

    public Armadura getArmadura() {
        return this.armadura;
    }

    public Categoria getCategoriaArmadura() {
        return this.getArmadura().getCategoria();
    }

    public int getNivelArmadura() {
        return this.getArmadura().getCategoria().getValor();
    }

    public String getNomeConstelacaoArmadura() {
        return this.getArmadura().getConstelacao().getNomeConstelacao();
    }

    public Genero getGenero() {
        return this.genero;
    }

    public void setGenero(Genero genero) {
        this.genero = genero;
    }

    public Status getStatus() {
        return this.status;
    }

    public double getVida() {
        return this.vida;
    }

    public int getQtdSentidosDespertados() {
        return this.qtdSentidosDespertados;
    }

    public ArrayList <Movimento> getMovimentos () {
        return this.movimentos;
    }

    public void perderVida(double perda) throws Exception {
        if(perda < 0) {
            throw new InvalidParameterException();
        }
        if(this.vida<1 || perda>=this.vida) {
            this.status = Status.MORTO;
            this.vida = 0;
        }
        if(this.status != Status.MORTO){
            this.vida-=perda;
        }
    }

    public Constelacao getConstelacao() {
        return this.armadura.getConstelacao();
    }

    public ArrayList <Golpe> getGolpes() {
        return this.getConstelacao().getGolpes();
    }

    public void aprenderGolpe(Golpe golpe) {
        Constelacao constelacaoSaint = this.getConstelacao();
        constelacaoSaint.adicionarGolpe(golpe);
    }

    public Golpe getProximoGolpe() {
        ArrayList <Golpe> golpes = this.getGolpes();
        int posicao = this.acumuladorProximo % golpes.size();
        this.acumuladorProximo++;
        return golpes.get(posicao);
    }

    public void adicionarMovimento(Movimento move) { 
        movimentos.add(move);
    }

    public Movimento getProximoMovimento() {
        int posicao = this.acumuladorProximo % this.movimentos.size();
        this.acumuladorProximo++;
        return this.movimentos.get(posicao);
    }

    public String getCSV() {
        return this.getNome() + "," + this.getVida() + "," + this.getNomeConstelacaoArmadura() + "," + this.getCategoriaArmadura()
        + "," + this.getStatus() + "," + this.getGenero() + "," + this.getArmaduraVestida();
    }

}