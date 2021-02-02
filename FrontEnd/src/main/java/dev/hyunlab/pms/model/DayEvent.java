package dev.hyunlab.pms.model;

import java.io.Serializable;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.OneToOne;
import javax.persistence.Table;

import com.fasterxml.jackson.annotation.JsonProperty;

import org.hibernate.annotations.DynamicUpdate;

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
@Table(name = "day_event")
@DynamicUpdate
public class DayEvent implements Serializable {

    /**
     *
     */
    private static final long serialVersionUID = 1L;

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(updatable = false, nullable = false, columnDefinition = "INT(11)")
    @JsonProperty("Id")
    private Long id;

    @Column(name = "de", nullable = false, length = 8, columnDefinition = "VARCHAR(8)")
    @JsonProperty("De")
    private String de;

    @Column(name = "event_title", length = 255, columnDefinition = "VARCHAR(255)")
    @JsonProperty("EventTitle")
    private String eventTitle;


    @Column(name = "event_cn", length = 4000, columnDefinition = "VARCHAR(4000)")
    @JsonProperty("EventCn")
    private String eventCn;

    @Column(name = "user_id", length = 50, columnDefinition = "VARCHAR(50)", insertable = false, updatable = false)
    @JsonProperty("UserId")
    private String userId;

    @OneToOne(targetEntity = User.class, fetch = FetchType.LAZY)
    @JoinColumn(name = "user_id")
    @JsonProperty("User")
    private User user;
    
}
