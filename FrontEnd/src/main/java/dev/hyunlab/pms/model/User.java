package dev.hyunlab.pms.model;

import java.io.Serializable;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.Table;

import com.fasterxml.jackson.annotation.JsonFormat;
import com.fasterxml.jackson.annotation.JsonProperty;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Entity
@Table(name = "user")
public class User implements Serializable {

    /**
     *
     */
    private static final long serialVersionUID = 1L;

    @Id
    @Column(name = "user_id", length = 50, columnDefinition = "VARCHAR(50)")
    @JsonProperty(value = "UserId")
    private String userId;
    
    @Column(name = "user_name", length = 50, columnDefinition = "VARCHAR(50)")
    @JsonProperty(value = "UserName")
    private String userName;

    @Column(name = "passwd", length = 255, columnDefinition = "VARCHAR(255")
    @JsonProperty(value = "Passwd")
    private String passwd;
    
}
