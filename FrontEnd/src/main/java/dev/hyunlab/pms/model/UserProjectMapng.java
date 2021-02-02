package dev.hyunlab.pms.model;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.Table;

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
@Table(name = "user_project_mapng")
public class UserProjectMapng {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "user_project_mapng_id", insertable = false)
    @JsonProperty(value = "UserProjectMapngId")
    private int userProjectMapngId;

    // @Column(name = "user_id", length = 50, columnDefinition = "VARCHAR(50)")
    
    @ManyToOne()
    @JoinColumn(name = "user_id")
    @JsonProperty(value = "User")
    private User user;

    // @Column(name = "project_id")
    @ManyToOne()
    @JoinColumn(name = "project_id")
    @JsonProperty(value = "Project")
    private Project project;
}
