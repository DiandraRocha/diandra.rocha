package br.com.crescer.imovie.entidade;

import java.io.Serializable;
import java.math.BigDecimal;
import javax.persistence.Basic;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.Table;
import javax.validation.constraints.NotNull;

/**
 *
 * @author diandra.rocha
 */
@Entity
@Table(name = "CURTIDA")
public class Curtida implements Serializable {

    private static final long serialVersionUID = 1L;
    
    @Id
    @Basic(optional = false)
    @NotNull
    @Column(name = "IDCURTIDA")
    private long idcurtida;
    
    @JoinColumn(name = "IDPOST", referencedColumnName = "IDPOST")
    @ManyToOne(optional = false)
    private Post idpost;
    
    @JoinColumn(name = "IDUSUARIO", referencedColumnName = "IDUSUARIO")
    @ManyToOne(optional = false)
    private Usuario idusuario;

    public Curtida() {
    }

    public Curtida(Usuario user, Post post) {
        this.idusuario = user;
        this.idpost = post;
    }

    public long getIdcurtida() {
        return idcurtida;
    }

    public void setIdcurtida(long idcurtida) {
        this.idcurtida = idcurtida;
    }

    public Post getIdpost() {
        return idpost;
    }

    public void setIdpost(Post idpost) {
        this.idpost = idpost;
    }

    public Usuario getIdusuario() {
        return idusuario;
    }

    public void setIdusuario(Usuario idusuario) {
        this.idusuario = idusuario;
    }
    
}